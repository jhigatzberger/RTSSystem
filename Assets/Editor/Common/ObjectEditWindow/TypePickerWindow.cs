using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TypePickerWindow : EditorWindow
{
    public enum EditType
    {
        New,
        Reference,
        Copy
    }
    private VisualElement editorContainer;
    private TypePicker typePicker;
    private EditType _currentEditType;

    private EditType CurrentEditType
    {
        get => _currentEditType;
        set
        {
            _currentEditType = value;
            OnEditTypeChanged();            
        }
    }
    private void OnEditTypeChanged()
    {
        if (typePicker != null)
            editorContainer.Remove(typePicker);
        typePicker = editTypeMap[CurrentEditType]();
        editorContainer.Add(typePicker);
    }

    private Dictionary<EditType, Func<TypePicker>> editTypeMap;

    public static void Show<T>(Action<T> callback, UnityEngine.Object parent = null, EditType[] editTypes = null) where T : UnityEngine.Object
    {
        TypePickerWindow window = CreateInstance<TypePickerWindow>();
        window.Initialize(callback, parent, editTypes);
        window.Show();
    }
    private void Initialize<T>(Action<T> callback, UnityEngine.Object parent, EditType[] editTypes) where T : UnityEngine.Object
    {
        typePicker = null;
        if (editTypes == null)
            editTypes = Enum.GetValues(typeof(EditType)).OfType<EditType>().ToArray();

        Button saveButton = new Button(() => Save(callback, parent));
        saveButton.text = "Confirm";

        editorContainer = new VisualElement();

        bool hasReferences = Resources.LoadAll<T>("").Length > 0;

        editTypeMap = new Dictionary<EditType, Func<TypePicker>>();
        if (editTypes.Contains(EditType.New))
            editTypeMap.Add(EditType.New, () => new NewTypePicker<T>());
        if (editTypes.Contains(EditType.Reference) && hasReferences)
            editTypeMap.Add(EditType.Reference, () => new ReferenceTypePicker<T>());
        if (editTypes.Contains(EditType.Copy) && hasReferences)
            editTypeMap.Add(EditType.Copy, () => new CopyTypePicker<T>());



        if (editTypes.Length > 1)
        {
            VisualElement typePopupFieldContainer = new VisualElement();
            PopupField<string> typePopupField = new PopupField<string>(editTypes.Select(s => s.ToString()).ToList(), 0,
            (s) =>
            {
                CurrentEditType = (EditType)Enum.Parse(typeof(EditType), s);
                return s;
            });
            typePopupFieldContainer.Add(new Label("Edit Type: "));
            typePopupFieldContainer.Add(typePopupField);
            rootVisualElement.Add(typePopupFieldContainer);
        }
        else
            CurrentEditType = editTypes[0];
        rootVisualElement.Add(editorContainer);
        rootVisualElement.Add(saveButton);
    }

    private void Save<T>(Action<T> callback, UnityEngine.Object parent) where T : UnityEngine.Object
    {
        if (parent != null && CurrentEditType != EditType.Reference)
        {
            AssetDatabase.AddObjectToAsset(typePicker.Cache, parent);
            EditorUtility.SetDirty(typePicker.Cache);
            EditorUtility.SetDirty(parent);
            AssetDatabase.SaveAssets();
        }
        if (callback != null)
            callback(typePicker.Cache as T);
        Close();
    }
}
