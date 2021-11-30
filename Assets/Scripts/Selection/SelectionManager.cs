using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionManager
{
    [SerializeField] private LayerMask _groundLayerMask;
    public static LayerMask GroundLayerMask
    {
        get
        {
            return Instance._groundLayerMask;
        }
    }
    [SerializeField] private LayerMask _selectableLayerMask;
    public static LayerMask SelectableLayerMask
    {
        get
        {
            return Instance._selectableLayerMask;
        }
    }
    
    [SerializeField] Selector[] selectors;
    private void Update() make this a static class and have the selectors update for themselves?
    {
        if (Input.GetMouseButtonDown(0))
            UnSelectAll();
        foreach (Selector selector in selectors)
        {
            selector.UpdatePosition(Input.mousePosition); unnecessary
            if (Input.GetMouseButtonDown(0))
                selector.OnMouseDown();
            else if (Input.GetMouseButtonUp(0))
                selector.OnMouseUp();
        }
    }

    private static int _priority = 0;
    public static int Priority
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
                    foreach (SelectableObject selectableObject in selection)
                        if (selectableObject.Priority > _priority)
                            _priority = selectableObject.Priority;
            }
        }
    }
   
    public static void UnSelectAll()
    {
        Priority = 0;
        foreach (SelectableObject so in selection)
            so.OnDeselect();
        selection.Clear();
    }

    public static HashSet<SelectableObject> selection = new HashSet<SelectableObject>();
    public static HashSet<SelectableObject> visibleSelectableObjects = new HashSet<SelectableObject>();
    public void OnGUI() unnecessary
    {
        foreach (Selector selector in selectors)
            selector.OnGUI();
    }
}
