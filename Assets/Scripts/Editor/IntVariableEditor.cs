using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntVariable))]
public class IntVariableEditor : Editor
{
    SerializedProperty m_IntProp;

    private void OnEnable()
    {
        m_IntProp = serializedObject.FindProperty("_value");
    }

    public override void OnInspectorGUI() {
        IntVariable variable = target as IntVariable;
        EditorGUILayout.PropertyField(m_IntProp, new GUIContent("Int Field"), GUILayout.Height(20));
        if (GUILayout.Button("Increment"))
        {
            variable.Increment();
        }
        if (GUILayout.Button("Decrement"))
        {
            variable.Decrement();
        }
        serializedObject.ApplyModifiedProperties();
            
    }
}
