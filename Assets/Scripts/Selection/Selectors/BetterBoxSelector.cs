using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterBoxSelector : Selector
{
    #region Config
    [Tooltip("minimum x and y distances to avoid dynamic mesh creation errors")]
    [SerializeField] private float selectionThreshhold = 2;

    [Header("GUI Box")]
    [SerializeField] private Color borderColor = new Color(0.8f, 0.8f, 0.95f);
    [SerializeField] private Color innerColor = new Color(0.8f, 0.8f, 0.95f, 0.25f);
    [SerializeField] private float borderThickness;
    #endregion

    protected override bool Applicable
    {
        get
        {
            if (startPos.HasValue)
            {
                float threshhold = Mathf.Clamp(selectionThreshhold, 2, 100);
                return Mathf.Abs(Input.mousePosition.x - startPos.Value.x) > threshhold && Mathf.Abs(Input.mousePosition.y - startPos.Value.y) > threshhold;
            }
            return false;
        }
    }

    private Vector2? startPos;
    private HashSet<SelectableObject> hoveredSelectableObjects = new HashSet<SelectableObject>();
    private int priority = 0;

    public override void Down()
    {
        startPos = Input.mousePosition;
    }

    public override void Up()
    {
        startPos = null;
        Manager.Selection = new Selection(hoveredSelectableObjects, priority);
        hoveredSelectableObjects.Clear();
    }

    private void Update()
    {
        if (Applicable)
        {
            Bounds selectionBounds = GetViewportBounds( startPos.Value, Input.mousePosition );
            bool inBounds;
            priority = 0;
            print(hoveredSelectableObjects.Count);
            foreach (SelectableObject selectableObject in SelectableObject.onScreen)
            {
                inBounds = selectionBounds.Contains(Camera.main.WorldToViewportPoint(selectableObject.transform.position));
                if (inBounds)
                {
                    hoveredSelectableObjects.Add(selectableObject);
                    if (selectableObject.ownPriority > priority)
                        priority = selectableObject.ownPriority;
                }
                else
                    hoveredSelectableObjects.Remove(selectableObject);
                selectableObject.UpdateIndicator(inBounds);
            }
            Manager.Selection.Priority = priority;
        }
    }

    private void OnGUI()
    {
        if (Applicable)
        {
            Rect rect = ScreenDrawingUtils.GetScreenRect(startPos.Value, Input.mousePosition);
            ScreenDrawingUtils.DrawScreenRect(rect, innerColor);
            ScreenDrawingUtils.DrawScreenRectBorder(rect, borderThickness, borderColor);
        }
    }

    public static Bounds GetViewportBounds(Vector2 screenPosition1, Vector2 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = Camera.main.nearClipPlane;
        max.z = Camera.main.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }

}
