using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyClickSelector", menuName = "RTS/Selection/Selectors/ClickSelector", order = 1)]
public class ClickSelector : Selector
{
    [Tooltip("default value is the exact inverse of the boxselect threshhold")]
    [SerializeField] private float selectionThreshhold = 2;
    protected override bool Applicable
    {
        get
        {
            if (Position.HasValue && startPos.HasValue)
                return Mathf.Abs(Position.Value.x - startPos.Value.x) < selectionThreshhold || Mathf.Abs(Position.Value.y - startPos.Value.y) < selectionThreshhold;
            return false;
        }
    }

    private Vector2? startPos;
    public override void OnMouseDown()
    {
        startPos = Position;
    }

    public override void OnMouseUp()
    {
        if (Applicable)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Position.Value);
            if (Physics.Raycast(ray, out hit, float.MaxValue, SelectionManager.SelectableLayerMask))
            {
                SelectableObject selectableObject = hit.collider.GetComponent<SelectableObject>();
                if(selectableObject != null)
                    selectableObject.Select();
            }
            Debug.Log(hit);
        }
    }
}
