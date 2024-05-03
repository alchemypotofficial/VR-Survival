using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    public XRController controller = null;

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateHandAnimation();
    }

    private void UpdateHandAnimation()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            animator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            animator.SetFloat("Trigger", 0);
        }

        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            animator.SetFloat("Grip", gripValue);
        }
        else
        {
            animator.SetFloat("Grip", 0);
        }
    }
}
