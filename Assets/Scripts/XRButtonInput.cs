using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonInput : MonoBehaviour
{
    public XRController controller;
    public XRDirectInteractor interactor;
    public GameObject player;

    private GameObject currentHeldObject;

    void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {
        if (controller.inputDevice.isValid)
        {
            InputDevice device = controller.inputDevice;

            if (interactor.selectTarget)
            {
                currentHeldObject = interactor.selectTarget.gameObject;

                if (currentHeldObject.GetComponent<XRButtonInteraction>())
                {
                    XRButtonInteraction buttonInteraction = currentHeldObject.GetComponent<XRButtonInteraction>();

                    bool primaryButton;
                    // If primary button is pressed
                    if (device.IsPressed(InputHelpers.Button.PrimaryButton, out primaryButton) && primaryButton)
                    {
                        buttonInteraction.PrimaryButtonDown();
                        Debug.Log("Primary button pressed by " + player.name);
                    }
                    else
                    {
                        buttonInteraction.PrimaryButtonUp();
                    }

                    bool secondaryButton;
                    // If secondary button is pressed
                    if (device.IsPressed(InputHelpers.Button.SecondaryButton, out secondaryButton) && secondaryButton)
                    {
                        buttonInteraction.SecondaryButtonDown();
                        Debug.Log("Secondary button pressed by " + player.name);
                    }
                    else
                    {
                        buttonInteraction.SecondaryButtonUp();
                    }

                    float triggerValue;
                    // If trigger is pressed
                    if (device.TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
                    {
                        if (triggerValue > 0.5)
                        {
                            buttonInteraction.TriggerPulled();
                            Debug.Log("Trigger button pressed by " + player.name);
                        }
                        else
                        {
                            buttonInteraction.TriggerReleased();
                        }
                    }
                    else
                    {
                        buttonInteraction.TriggerReleased();
                    }
                }
            }
            else
            {
                currentHeldObject = null;
            }
        }
    }
}
