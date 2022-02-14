using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using JHiga.RTSEngine;

[CustomEditor(typeof(ExtensionFactory), true, isFallback = true)]
public class CustomExtensionFactoryEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        inspector.Add(new NameField(target));
        inspector.Add(new DefaultInspectorElement(serializedObject));
        return inspector;
    }
}