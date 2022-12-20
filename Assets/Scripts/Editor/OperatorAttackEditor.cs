using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OperatorAttack))]
public class OperatorAttackEditor : Editor
{
     SerializedProperty AttackTilePrefab;
     bool showBtn = true;
       void OnEnable()
       {
           AttackTilePrefab = serializedObject.FindProperty("attackTilePrefab");
       }
   
       int selGridInt = 0;
       string[] selStrings = {"radio1", "radio2", "radio3"};

       public override void OnInspectorGUI()
       {
          
               GUILayout.BeginVertical("Box");
               selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1);
               if (GUILayout.Button("Start"))
               {
                   Debug.Log("You chose " + selStrings[selGridInt]);
               }
               showBtn = EditorGUILayout.Toggle("Show Button", showBtn);
               showBtn = EditorGUILayout.Toggle("Show Button", showBtn);

               GUILayout.EndVertical();
           
           serializedObject.Update();
           EditorGUILayout.PropertyField(AttackTilePrefab);
           serializedObject.ApplyModifiedProperties();
           
           
       }
}
