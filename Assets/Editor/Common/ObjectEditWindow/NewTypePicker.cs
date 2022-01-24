
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NewTypePicker<T> : TypePicker where T: Object
{
    protected Object _cache;
    public override Object Cache
    {
        get => _cache;
        set
        {
            if (value != _cache)
            {
                _cache = value;
                if (value != null)
                    CacheEditor = Editor.CreateEditor(value);
                else
                    CacheEditor = null;
            }
        }
    }
    private VisualElement InspectorContainer
    {
        get
        {
            if(_inspectorContainer == null)
            {
                _inspectorContainer = new VisualElement();
                Add(InspectorContainer);
            }
            return _inspectorContainer;
        }
    }
    private VisualElement _inspectorContainer;
    private Editor CacheEditor
    {
        set
        {
            InspectorContainer.Clear();
            if(value != null)
                InspectorContainer.Add(value.CreateInspectorGUI());
        }
    }
    public NewTypePicker() : base(typeof(T))
    {
    }
    protected override void OnTypeChanged()
    {
        ScriptableObject newSO = ScriptableObject.CreateInstance(CurrentType);
        newSO.name = "New " + CurrentType.Name;
        Cache = newSO;
    }
}
