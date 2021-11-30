using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Selector[] selectors;
    void Awake()
    {
        selectors = GetComponents<Selector>();
    }

    #region Selection
    private Selection _selection = new Selection();
    public Selection Selection {
        set
        {
            if (value != null)
                _selection = value;
            else
                _selection = new Selection();
        }
        get
        {
            return _selection;
        }       
    }
    #endregion

    #region LayerMasks
    public LayerMask groundLayerMask;
    public LayerMask selectableLayerMask;
    #endregion

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Selection.Clear();
            foreach (Selector selector in selectors)
                selector.Down();
        }
        else if (Input.GetMouseButtonUp(0))
            foreach (Selector selector in selectors)
                selector.Up();
    }
}
