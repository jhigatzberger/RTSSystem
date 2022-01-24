using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
public abstract class TypePicker : VisualElement
{
    public virtual UnityEngine.Object Cache { get; set; }
    protected VisualElement typeContainer;
    protected Type _currentType;
    protected Type CurrentType
    {
        get => _currentType;
        set
        {
            if (value != _currentType)
            {
                _currentType = value;
                OnTypeChanged();
            }
        }
    }
    protected abstract void OnTypeChanged();
    protected Dictionary<string, Type> nameToType;
    public TypePicker(Type type)
    {
        InitTypes(type);
        BuildElement();
    }
    private void InitTypes(Type parentType)
    {
        List<Type> typeList = new List<Type>(TypeCache.GetTypesDerivedFrom(parentType).Where(t => !t.IsAbstract));
        if (!parentType.IsAbstract)
            typeList.Add(parentType);
        nameToType = new Dictionary<string, Type>();
        foreach (Type t in typeList)
            nameToType.Add(t.Name, t);
    }
    protected virtual void BuildElement()
    {
        if (nameToType.Values.Count > 1)
        {
            Add(typeContainer = new VisualElement());
            PopupField<string> typePopupField = new PopupField<string>(nameToType.Keys.ToList(), 0,
                (s) =>
                {
                    CurrentType = nameToType[s];
                    return s;
                }
            );
            typeContainer.Add(new Label("Type: "));
            typeContainer.Add(typePopupField);
        }
        else
            CurrentType = nameToType.Values.First();
    }
}