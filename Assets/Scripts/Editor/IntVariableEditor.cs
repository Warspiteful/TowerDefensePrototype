using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntVariable))]
public class IntVariableEditor : Editor
{
    
    public override void OnInspectorGUI() {
        
        IntVariable variable = target as IntVariable;
        if (GUILayout.Button("Increment"))
        {
            variable.Increment();
        }
        if (GUILayout.Button("Decrement"))
        {
            variable.Decrement();
        }
            
    }
}
