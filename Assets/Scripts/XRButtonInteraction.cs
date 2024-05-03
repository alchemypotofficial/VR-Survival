using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XRButtonInteraction : MonoBehaviour
{
    [Header("Input Interaction")]
    public UnityEvent triggerPulled;
    public UnityEvent triggerReleased;
    public UnityEvent primaryButtonDown;
    public UnityEvent primaryButtonUp;
    public UnityEvent secondaryButtonDown;
    public UnityEvent secondaryButtonUp;

    public void TriggerPulled()
    {
        triggerPulled.Invoke();
    }

    public void TriggerReleased()
    {
        triggerReleased.Invoke();
    }

    public void PrimaryButtonDown()
    {
        primaryButtonDown.Invoke();
    }

    public void PrimaryButtonUp()
    {
        primaryButtonUp.Invoke();
    }

    public void SecondaryButtonDown()
    {
        secondaryButtonDown.Invoke();
    }

    public void SecondaryButtonUp()
    {
        secondaryButtonUp.Invoke();
    }
}
