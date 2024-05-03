using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRTwoHandGrabInteractable : XRGrabInteractable
{
    public List<XRSecondHandInteractble> secondaryGrabPoints = new List<XRSecondHandInteractble>();

    private XRBaseInteractor secondaryInteractor;
    private Quaternion attachInitialRotation;

    private Vector3 interactorPosition = Vector3.zero;
    private Quaternion interactorRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var point in secondaryGrabPoints)
        {
            point.selectEntered.AddListener(OnSecondaryGrab);
            point.selectExited.AddListener(OnSecondaryRelease);

            point.gameObject.SetActive(false);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondaryInteractor && selectingInteractor)
        {
            // Rotate object by 'secondaryInteractor'
            Quaternion newRotation = Quaternion.LookRotation(selectingInteractor.attachTransform.position - secondaryInteractor.attachTransform.position);
            Vector3 eulerAngles = newRotation.eulerAngles;
            eulerAngles.y -= 90f;
            selectingInteractor.attachTransform.rotation = Quaternion.Euler(eulerAngles);
        }

        base.ProcessInteractable(updatePhase);
    }

    public void OnSecondaryGrab(SelectEnterEventArgs eventArgs)
    {
        secondaryInteractor = eventArgs.interactor;
    }

    public void OnSecondaryRelease(SelectExitEventArgs eventArgs)
    {
        secondaryInteractor = null;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        attachInitialRotation = args.interactor.attachTransform.localRotation;

        foreach (var point in secondaryGrabPoints)
        {
            point.gameObject.SetActive(true);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        secondaryInteractor = null;
        args.interactor.attachTransform.localRotation = attachInitialRotation;

        foreach (var point in secondaryGrabPoints)
        {
            point.gameObject.SetActive(false);
        }
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
