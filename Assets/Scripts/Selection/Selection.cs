using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection
{
    public HashSet<SelectableObject> items;
    private int _priority;
    public Selection(ICollection<SelectableObject> items = null, int priority = -1)
    {
        if(items == null)
            items = new HashSet<SelectableObject>();
        if(priority < 0)
            priority = FindPriority(items);
        _priority = priority;
        this.items = new HashSet<SelectableObject>(items);
    }

    private static int FindPriority(ICollection<SelectableObject> items)
    {
        int priority = 0;
        foreach (SelectableObject selectableObject in items)
            if (selectableObject.ownPriority > priority)
                priority = selectableObject.ownPriority;
        return priority;
    }

    public int Priority
    {
        get
        { 
            return _priority;
        }
        set
        {
            if(value != _priority)
            {
                _priority = value;
                if (_priority < 0)
                    _priority = FindPriority(items);
                SelectableObject.selectedPriority = value;
            }
        }
    }

    public void Add(SelectableObject selectableObject)
    {
        if (selectableObject == null)
            return;

        if (items.Add(selectableObject) && selectableObject.ownPriority > Priority)
            Priority = selectableObject.ownPriority;

        selectableObject.OnSelect();
    }
    public void Remove(SelectableObject selectableObject)
    {
        if (selectableObject == null)
            return;

        if (items.Remove(selectableObject) && selectableObject.ownPriority == Priority)
            Priority = -1;

        selectableObject.OnDeselect();
    }

    public void Clear()
    {
        Priority = 0;
        foreach (SelectableObject selectableObject in items)
            selectableObject.OnDeselect();
        items.Clear();
    }

}
