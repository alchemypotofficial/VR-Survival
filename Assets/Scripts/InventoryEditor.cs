using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Inventory inventory = (Inventory)target;

        if (GUILayout.Button("Turn Page Left"))
        {
            inventory.ShiftPageLeft();
        }

        if (GUILayout.Button("Turn Page Right"))
        {
            inventory.ShiftPageRight();
        }

        if (GUILayout.Button("Add Item"))
        {
            inventory.AddItem(Instantiate(inventory.itemToAdd));
        }

        if (GUILayout.Button("Refresh Page"))
        {
            inventory.RefreshPage();
        }
    }
}
