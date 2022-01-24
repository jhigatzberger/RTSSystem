using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class DefaultInspectorElement : VisualElement
{
    public DefaultInspectorElement(SerializedObject serializedObject)
    {
        Add(new NameField(serializedObject.targetObject));
        foreach (FieldInfo f in serializedObject.targetObject.GetType().GetFields())
        {
            SerializedProperty property = serializedObject.FindProperty(f.Name);
            if (property == null)
                return;
            PropertyField propertyField = new PropertyField();
            propertyField.Bind(serializedObject);
            propertyField.BindProperty(property);
            Add(propertyField);
        }
    }
}
