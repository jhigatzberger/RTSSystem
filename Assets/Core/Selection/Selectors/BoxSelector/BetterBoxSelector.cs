using UnityEngine;
using JHiga.RTSEngine.Selection;

public class BetterBoxSelector : Selector
{
    #region Config       
    [SerializeField] private float minMouseDistance = 2;
    [Header("GUI Box")]
    [SerializeField] private Color borderColor = new Color(0.8f, 0.8f, 0.95f);
    [SerializeField] private Color innerColor = new Color(0.8f, 0.8f, 0.95f, 0.25f);
    [SerializeField] private float borderThickness;
    #endregion
    public override int Priority => 2;
    protected override bool Applicable
    {
        get
        {
            if (startPos.HasValue)
                return Vector2.Distance(startPos.Value, Input.mousePosition)>minMouseDistance;
            return false;
        }
    }
    private Vector2? startPos;
    public override void InputStart()
    {
        startPos = Input.mousePosition;
    }
    public override void InputStop()
    {
        startPos = null;
    }
    private void Update()
    {
        if (Applicable)
            UpdateSelectionToBounds(GetViewportBounds(startPos.Value, Input.mousePosition));
    }
    private void UpdateSelectionToBounds(Bounds selectionBounds)
    {
        foreach (ISelectable selectableObject in SelectionContext.onScreen)
        {
            if (selectionBounds.Contains(Camera.main.WorldToViewportPoint(selectableObject.Entity.MonoBehaviour.transform.position)) && SelectionContext.Priority <= selectableObject.Priority)
                SelectionContext.Select(selectableObject);
            else
                SelectionContext.Deselect(selectableObject);
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
