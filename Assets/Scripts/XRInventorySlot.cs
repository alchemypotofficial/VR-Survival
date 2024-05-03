using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInventorySlot : XRSocketInteractor
{
    public GameObject currentItem;
    public Inventory inventory;
    public int slotIndex;

    private void Start()
    {
        selectEntered.AddListener(ItemObjectPlaced);
        selectExited.AddListener(ItemObjectRemoved);
    }

    public void SetItem(GameObject itemObject)
    {
        XRGrabInteractable interactable = itemObject.GetComponent<XRGrabInteractable>();

        if(interactable)
        {
            currentItem = itemObject;

            itemObject.transform.parent = transform;
            itemObject.transform.position = attachTransform.position;
            itemObject.SetActive(true);

            interactionManager.ForceSelect(this, interactable);
        }
    }

    public void HideObject()
    {
        if(selectTarget)
        {
            selectTarget.gameObject.transform.parent = transform;
            selectTarget.gameObject.SetActive(false);
        }
    }

    private void ItemObjectPlaced(SelectEnterEventArgs args)
    {
        if(inventory.AreItemsEditable())
        {
            inventory.AddItem(args.interactable.gameObject, slotIndex + inventory.PageOffset());
        }
    }

    private void ItemObjectRemoved(SelectExitEventArgs args)
    {
        if(inventory.AreItemsEditable())
        {
            inventory.RemoveItem(slotIndex + inventory.PageOffset());
        }
    }
}