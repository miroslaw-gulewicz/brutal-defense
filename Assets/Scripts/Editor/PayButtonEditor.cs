using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PayButton))]
public class PayButtonEditor : UnityEditor.UI.ButtonEditor
{
    private SerializedProperty valuePropery;
    private SerializedProperty valueTextPropery;

    protected override void OnEnable()
    {
        base.OnEnable();

        // or any other private field
        valuePropery = serializedObject.FindProperty("value");
        valueTextPropery = serializedObject.FindProperty("valueText");
    }

    public override void OnInspectorGUI()
    {
        PayButton component = (PayButton)target;

        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(valuePropery);
        EditorGUILayout.PropertyField(valueTextPropery);

        serializedObject.ApplyModifiedProperties();

    }

}
