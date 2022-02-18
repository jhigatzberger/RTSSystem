using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NameField : VisualElement
{
    public NameField(Object target)
    {
        TextField nameField = new TextField("Name: ");
        nameField.value = target.name;
        nameField.RegisterCallback<ChangeEvent<string>>(e =>
        {
            target.name = e.newValue;
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        });
        Add(nameField);
    }
}
