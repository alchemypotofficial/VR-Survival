using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(BoxCollider))]
public class TapButton : MonoBehaviour
{
    public RawImage buttonImage;
    public Color pressedColor;
    public UnityEvent onPress;

    private Color defaultColor;
    private bool buttonDown = false;

    private void Start()
    {
        if(buttonImage)
        {
            defaultColor = buttonImage.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(buttonDown == false)
        {
            TouchCollider touchCollider = other.gameObject.GetComponent<TouchCollider>();

            if (touchCollider)
            {
                Debug.Log("Button touched!");
                onPress.Invoke();
                buttonDown = true;

                if(buttonImage)
                {
                    buttonImage.color = pressedColor;
                }

                StartCoroutine(ButtonTriggered());
            }
        }
    }

    private IEnumerator ButtonTriggered()
    {
        yield return new WaitForSeconds(0.2f);
        buttonDown = false;
        
        if(buttonImage)
        {
            buttonImage.color = defaultColor;
        }
    }
}
