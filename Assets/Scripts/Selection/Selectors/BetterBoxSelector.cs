using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyBoxSelector", menuName = "RTS/Selection/Selectors/BoxSelector", order = 1)]
public class BetterBoxSelector : Selector
{
    [Tooltip("minimum x and y distances to avoid dynamic mesh creation errors")]
    [SerializeField] private float selectionThreshhold = 2;
    protected override bool Applicable
    {
        get
        {
            if (Position.HasValue && startPos.HasValue)
            {
                float threshhold = Mathf.Clamp(selectionThreshhold, 2, 100);
                return Mathf.Abs(Position.Value.x - startPos.Value.x) > threshhold && Mathf.Abs(Position.Value.y - startPos.Value.y) > threshhold;
            }
            return false;
        }
    }

    [Header("GUI Box")]
    [SerializeField] private Color borderColor = new Color(0.8f, 0.8f, 0.95f);
    [SerializeField] private Color innerColor = new Color(0.8f, 0.8f, 0.95f, 0.25f);
    [SerializeField] private float borderThickness;

    private Vector2? startPos;

    public override void OnMouseDown()
    {
        startPos = Position;
    }

    private HashSet<SelectableObject> hoveredSelectableObjects = new HashSet<SelectableObject>();
    public override void OnMouseUp()
    {
        startPos = null;
        foreach (SelectableObject selectableObject in hoveredSelectableObjects)
        {
            selectableObject.ShouldShowIndicator = false;
            selectableObject.Select();
        }
        hoveredSelectableObjects.Clear();
    }

    public override void UpdatePosition(Vector2? pos)
    {
        base.UpdatePosition(pos);
        if (Applicable)
        {
            Bounds selectionBounds = GetViewportBounds( startPos.Value, Position.Value );
            bool inBounds;
            int priority = 0;
            foreach (SelectableObject selectableObject in SelectionManager.visibleSelectableObjects)
            {
                inBounds = selectionBounds.Contains(Camera.main.WorldToViewportPoint(selectableObject.transform.position));
                if (inBounds)
                {
                    hoveredSelectableObjects.Add(selectableObject);
                    if (selectableObject.Priority > priority)
                        priority = selectableObject.Priority;
                }
                else
                    hoveredSelectableObjects.Remove(selectableObject);
                selectableObject.ShouldShowIndicator = inBounds;
            }
            SelectionManager.Priority = priority;
        }
    }

    public override void OnGUI()
    {
        if (Applicable)
        {
            Rect rect = ScreenDrawingUtils.GetScreenRect(startPos.Value, Position.Value);
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
