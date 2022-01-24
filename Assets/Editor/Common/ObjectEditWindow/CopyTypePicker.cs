using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CopyTypePicker<T> : ReferenceTypePicker<T> where T : Object
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
    private VisualElement InspectorContainer { get =>  _inspectorContainer; }
    private VisualElement _inspectorContainer;
    private Editor CacheEditor
    {
        set
        {
            InspectorContainer.Clear();
            if (value != null)
                InspectorContainer.Add(value.CreateInspectorGUI());
        }
    }
    public CopyTypePicker() : base()
    {
    }

    protected override void BuildElement()
    {
        _inspectorContainer = new VisualElement();
        base.BuildElement();
        Add(InspectorContainer);
    }

    protected override void BuildReferencePopupField()
    {
        if (referencePopupField != null)
            referenceContainer.Remove(referencePopupField);
        referencePopupField = new PopupField<string>(_references.Select(r => r.name).ToList(), 0, (s) =>
        {
            Cache = Object.Instantiate(_references.First(r => r.name.Equals(s)));
            return s;
        });
        referenceContainer.Add(referencePopupField);
    }
}
