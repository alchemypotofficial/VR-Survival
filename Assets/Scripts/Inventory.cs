using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Inventory : MonoBehaviour
{
    public XRInventorySlot[] slots;
    public TextMesh pageInfo;
    public XRGrabInteractable backpackGrab;

    protected GameObject[] items;

    protected int currentPage = 0;
    protected int totalPages = 3;
    protected int slotsPerPage = 0;

    protected bool itemsEditable = true;
    protected bool itemsShown = false;

    public GameObject itemToAdd;

    private void Awake()
    {
        slotsPerPage = slots.Length;
        items = new GameObject[slotsPerPage * totalPages];

        int index = 0;
        foreach(XRInventorySlot slot in slots)
        {
            slot.slotIndex = index;
            index++;
        }

        RefreshPage();

        pageInfo.text = "Page " + (currentPage + 1) + "/" + totalPages;
    }

    private void Update()
    {
        if(IsBackpackGrabbed())
        {
            if(itemsShown == false)
            {
                itemsShown = true;

                ShowSlots();
                RefreshPage();
            }
        }
        else
        {
            if(itemsShown == true)
            {
                itemsShown = false;

                HideItems();
                HideSlots();
            }
        }
    }

    public void ItemsEditable(bool editable)
    {
        itemsEditable = editable;
    }

    public bool AreItemsEditable()
    {
        return itemsEditable;
    }

    public bool IsBackpackGrabbed()
    {
        if(backpackGrab.selectingInteractor)
        {
            if(backpackGrab.selectingInteractor.gameObject.GetComponent<XRController>())
            {
                return true;
            }
        }

        return false;
    }

    public bool AddItem(GameObject item)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = item;

                item.gameObject.SetActive(false);
                RefreshPage();

                return true;
            }
        }

        return false;
    }

    public bool AddItem(GameObject item, int index)
    {
        if(index >= 0 && index < items.Length - 1)
        {
            items[index] = item;

            item.gameObject.SetActive(false);
            RefreshPage();

            return true;
        }

        return false;
    }

    public void RemoveItem(int index)
    {
        if(index >= 0 && index < items.Length - 1)
        {
            items[index].transform.parent = null;
            items[index] = null;
        }
    }

    public void HideItems()
    {
        ItemsEditable(false);

        for(int i = 0; i < slots.Length; i++)
        {
            XRInventorySlot slot = slots[i];
            int currentIndex = i + (currentPage * slotsPerPage);

            slot.HideObject();
        }

        ItemsEditable(false);
    }

    public void HideSlots()
    {
        foreach(XRInventorySlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void ShowSlots()
    {
        foreach(XRInventorySlot slot in slots)
        {
            slot.gameObject.SetActive(true);
        }
    }

    public void ShiftPageRight()
    {
        if(currentPage < totalPages - 1)
        {
            currentPage++;

            RefreshPage();

            pageInfo.text = "Page " + (currentPage + 1) + "/" + totalPages;
        }
    }

    public void ShiftPageLeft()
    {
        if(currentPage > 0)
        {
            currentPage--;

            RefreshPage();

            pageInfo.text = "Page " + (currentPage + 1) + "/" + totalPages;
        }
    }

    public void RefreshPage()
    {
        ItemsEditable(false);

        for(int i = 0; i < slots.Length; i++)
        {
            XRInventorySlot slot = slots[i];
            int currentIndex = i + (currentPage * slotsPerPage);

            slot.HideObject();

            if (items[currentIndex] != null)
            {
                slot.SetItem(items[currentIndex]);
            }
        }

        ItemsEditable(true);
    }

    public int PageOffset()
    {
        return currentPage * slotsPerPage;
    }
}