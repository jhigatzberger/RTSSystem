using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(Object), true, isFallback = true)]
public class DefaultInspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        return new DefaultInspectorElement(serializedObject);
    }
}