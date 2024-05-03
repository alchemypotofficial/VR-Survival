using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public bool doDaylightCycle = true;

    [Range(0.0f, 1.0f)] public float time;
    public float fullDayLength = 15f;
    public float startTime = 0.4f;
    public Vector3 noon;

    private float timeRate;

    private int year = 1;
    private int month = 1;
    private int day = 1;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Day/Night Properties")]
    public Material blendedSky;
    public Material daySky;
    public Material nightSky;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultiplier;
    public Gradient fogColor;
    public AnimationCurve skyboxBlending;

    private void Start()
    {
        timeRate = 1.0f / (fullDayLength * 60f);
        time = startTime;

        if(startTime >= 0.25f && startTime <= 0.75f)
        {
            moon.intensity = 0;
            moon.gameObject.SetActive(false);

            sun.gameObject.SetActive(true);

            RenderSettings.skybox.SetColor("_SunDiscColor", daySky.GetColor("_SunDiscColor"));
            RenderSettings.skybox.SetFloat("_SunDiscMultiplier", daySky.GetFloat("_SunDiscExponent"));
            RenderSettings.skybox.SetFloat("_SunDiscExponent", daySky.GetFloat("_SunDiscExponent"));

            BlendSkybox();
        }
        else
        {
            sun.intensity = 0;
            sun.gameObject.SetActive(false);

            moon.gameObject.SetActive(true);

            RenderSettings.skybox.SetColor("_SunDiscColor", nightSky.GetColor("_SunDiscColor"));
            RenderSettings.skybox.SetFloat("_SunDiscMultiplier", nightSky.GetFloat("_SunDiscExponent"));
            RenderSettings.skybox.SetFloat("_SunDiscExponent", nightSky.GetFloat("_SunDiscExponent"));

            BlendSkybox();
        }
    }

    private void Update()
    {
        if(doDaylightCycle)
        {
            ProgressTime();
        }

        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);

        if(sun.intensity == 0f)
        {
            if(sun.gameObject.activeInHierarchy)
            {
                sun.gameObject.SetActive(false);
            }
        }
        else if(sun.intensity > 0f)
        {
            if(!sun.gameObject.activeInHierarchy)
            {
                sun.gameObject.SetActive(true);
            }

            if(RenderSettings.sun != sun)
            {
                RenderSettings.sun = sun;

                RenderSettings.skybox.SetColor("_SunDiscColor", daySky.GetColor("_SunDiscColor"));
                RenderSettings.skybox.SetFloat("_SunDiscMultiplier", daySky.GetFloat("_SunDiscExponent"));
                RenderSettings.skybox.SetFloat("_SunDiscExponent", daySky.GetFloat("_SunDiscExponent"));
            }
        }

        if(moon.intensity == 0f)
        {
            if(moon.gameObject.activeInHierarchy)
            {
                moon.gameObject.SetActive(false);
            }
        }
        else if(moon.intensity > 0f)
        {
            if(!moon.gameObject.activeInHierarchy)
            {
                moon.gameObject.SetActive(true);
            }

            if (RenderSettings.sun != moon)
            {
                RenderSettings.sun = moon;

                RenderSettings.skybox.SetColor("_SunDiscColor", nightSky.GetColor("_SunDiscColor"));
                RenderSettings.skybox.SetFloat("_SunDiscMultiplier", nightSky.GetFloat("_SunDiscExponent"));
                RenderSettings.skybox.SetFloat("_SunDiscExponent", nightSky.GetFloat("_SunDiscExponent"));
            }
        }

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
        RenderSettings.fogColor = fogColor.Evaluate(time);

        BlendSkybox();
    }

    public void BlendSkybox()
    {
        Material skyBox = RenderSettings.skybox;
        float blending = skyboxBlending.Evaluate(time);
        
        skyBox.SetColor("_SkyGradientTop", Color.Lerp(daySky.GetColor("_SkyGradientTop"), nightSky.GetColor("_SkyGradientTop"), blending));
        skyBox.SetColor("_SkyGradientBottom", Color.Lerp(daySky.GetColor("_SkyGradientBottom"), nightSky.GetColor("_SkyGradientBottom"), blending));
        skyBox.SetFloat("_SkyGradientExponent", Mathf.Lerp(daySky.GetFloat("_SkyGradientExponent"), nightSky.GetFloat("_SkyGradientExponent"), blending));

        skyBox.SetColor("_HorizonLineColor", Color.Lerp(daySky.GetColor("_HorizonLineColor"), nightSky.GetColor("_HorizonLineColor"), blending));
        skyBox.SetFloat("_HorizonLineExponent", Mathf.Lerp(daySky.GetFloat("_HorizonLineExponent"), nightSky.GetFloat("_HorizonLineExponent"), blending));
        skyBox.SetFloat("_HorizonLineContribution", Mathf.Lerp(daySky.GetFloat("_HorizonLineContribution"), nightSky.GetFloat("_HorizonLineContribution"), blending));

        skyBox.SetColor("_SunHaloColor", Color.Lerp(daySky.GetColor("_SunHaloColor"), nightSky.GetColor("_SunHaloColor"), blending));
        skyBox.SetFloat("_SunHaloExponent", Mathf.Lerp(daySky.GetFloat("_SunHaloExponent"), nightSky.GetFloat("_SunHaloExponent"), blending));
        skyBox.SetFloat("_SunHaloContribution", Mathf.Lerp(daySky.GetFloat("_SunHaloContribution"), nightSky.GetFloat("_SunHaloContribution"), blending));

        RenderSettings.skybox = skyBox;
    }

    public void ProgressTime()
    {
        time += timeRate * Time.deltaTime;

        if(time >= 1.0f)
        {
            time = 0.0f;
            day++;

            if(day > 31)
            {
                day = 1;
                month++;

                if(month > 12)
                {
                    month = 1;
                    year++;
                }
            }
        }
    }

    public TimePoint GetTime()
    {
        float currentHour;
        float currentMinute;
        float currentSecond;

        float totalDayLength = fullDayLength * 60f;

        float denormalizedTime = totalDayLength * time;

        currentHour = denormalizedTime / (totalDayLength / 24);
        currentMinute = 60 * (currentHour % 1f);
        currentSecond = 60 * (currentMinute % 1f);

        return new TimePoint(year, month, day, (int)currentHour, (int)currentMinute, (int)currentSecond);
    }
}

public class TimePoint
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int second;

    public TimePoint(int year, int month, int day, int hour, int minute, int second)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }
}