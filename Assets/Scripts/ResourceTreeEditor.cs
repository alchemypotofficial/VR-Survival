using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ResourceTree))]
public class ResourceTreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ResourceTree resource = (ResourceTree)target;

        if(GUILayout.Button("Destroy Resource"))
        {
            resource.DestroyResource();
        }
    }
}
