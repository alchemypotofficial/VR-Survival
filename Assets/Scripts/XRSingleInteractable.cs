using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSingleInteractable : XRGrabInteractable
{
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        XRController selectingController = null;

        if (selectingInteractor)
        {
            selectingController = selectingInteractor.gameObject.GetComponent<XRController>();
        }

        bool isAlreadyGrabbed = selectingInteractor && selectingController && !interactor.Equals(selectingInteractor) && interactor.gameObject.GetComponent<XRController>();
     
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
