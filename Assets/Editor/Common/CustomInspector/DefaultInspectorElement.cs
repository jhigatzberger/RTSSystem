using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DefaultInspectorElement : VisualElement
{
    public DefaultInspectorElement(SerializedObject serializedObject)
    {
        var iterator = serializedObject.GetIterator();
        if (iterator.NextVisible(true))
        {
            do
            {
                var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };
                propertyField.Bind(serializedObject);

                if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                    propertyField.SetEnabled(value: false);

                Add(propertyField);
            }
            while (iterator.NextVisible(false));
        }
    }
    /*
    private void ReflectionOption(SerializedObject serializedObject)
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
    }*/
}
