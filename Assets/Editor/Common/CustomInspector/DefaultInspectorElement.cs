using UnityEditor;
using UnityEditor.UIElements;
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
                propertyField.BindProperty(serializedObject);
                if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                    propertyField.SetEnabled(value: false);

                Add(propertyField);
            }
            while (iterator.NextVisible(false));
        }
    }
}
