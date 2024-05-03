using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DayNightCycle))]
public class DayNightCycleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DayNightCycle cycle = (DayNightCycle)target;

        if (GUILayout.Button("Get Time"))
        {
            TimePoint timePoint = cycle.GetTime();
            Debug.Log(timePoint.day + ", " + timePoint.hour + ", " + timePoint.minute + ", " + timePoint.second);
        }
    }
}
