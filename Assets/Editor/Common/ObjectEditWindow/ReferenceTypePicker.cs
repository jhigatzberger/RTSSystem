using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ReferenceTypePicker<T> :  TypePicker where T : UnityEngine.Object
{
    protected T[] _references;
    protected VisualElement referenceContainer;
    protected PopupField<string> referencePopupField;
    public ReferenceTypePicker() : base(typeof(T))
    {
    }
    protected override void BuildElement()
    {
        referenceContainer = new VisualElement();
        referenceContainer.Add(new Label("Reference: "));
        base.BuildElement();
        Add(referenceContainer);
        BuildReferencePopupField();
    }
    protected virtual void BuildReferencePopupField()
    {
        if (referencePopupField != null)
           referenceContainer.Remove(referencePopupField);
        referencePopupField = new PopupField<string>(_references.Select(r => r.name).ToList(), 0, (s) =>
        {
            Cache = _references.First(r => r.name.Equals(s));
            return s;
        });
        referenceContainer.Add(referencePopupField);
    }
    protected override void OnTypeChanged()
    {
        _references = Resources.LoadAll("", CurrentType).Select(r => (T)r).ToArray();
        BuildReferencePopupField();
    }
}
