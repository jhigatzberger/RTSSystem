using JHiga.RTSEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(FactionProperties))]
[CanEditMultipleObjects]
public class CustomFactionEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        inspector.Add(new NameField(target));
        PropertyField startEntitiesField = new PropertyField();
        startEntitiesField.Bind(serializedObject);
        startEntitiesField.BindProperty(serializedObject.FindProperty("startEntities"));
        inspector.Add(startEntitiesField);
        return inspector;
    }
}
