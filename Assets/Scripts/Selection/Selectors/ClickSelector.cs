using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSelector : Selector
{
    [Tooltip("default value is the exact inverse of the boxselect threshhold")]
    [SerializeField] private float selectionThreshhold = 2;
    protected override bool Applicable
    {
        get
        {
            if (startPos.HasValue)
                return Mathf.Abs(Input.mousePosition.x - startPos.Value.x) < selectionThreshhold || Mathf.Abs(Input.mousePosition.y - startPos.Value.y) < selectionThreshhold;
            return false;
        }
    }

    private Vector2? startPos;
    public override void Down()
    {
        startPos = Input.mousePosition;
    }

    public override void Up()
    {
        if (Applicable)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue, Manager.selectableLayerMask))
                Manager.Selection.Add(hit.collider.GetComponent<SelectableObject>());
            Debug.Log(hit);
        }
    }
}
