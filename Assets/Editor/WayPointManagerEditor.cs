using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(WayPointManager))]
public class WayPointManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        // Update the serialize object
        serializedObject.Update();
/*
        EditorGUILayout.Proper
        // Display properties
        EditorGUILayout.PropertyField(serializedObject.FindProperty("customClassList"), true);*/

        // Apply modif
        serializedObject.ApplyModifiedProperties();
    }

}
