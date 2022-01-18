using UnityEngine;
using UnityEditor;
using JHiga.RTSEngine;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(EntityGroup))]
[CanEditMultipleObjects]
public class CustomGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        serializedObject.targetObject.name = EditorGUILayout.TextField("Name: ", serializedObject.targetObject.name);
        serializedObject.ApplyModifiedProperties();
    }
}