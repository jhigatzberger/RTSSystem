using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Object), true, isFallback = true)]
public class DefaultInspector : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        inspector.Add(new NameField(target));
        inspector.Add(new DefaultInspectorElement(serializedObject));
        return inspector;
    }
}