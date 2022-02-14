using JHiga.RTSEngine;
using UnityEngine;
namespace JHiga.RTSEngine.Selection
{
    public class ClickSelector : Selector
    {
        [Tooltip("default value is the exact inverse of the boxselect threshhold")]
        [SerializeField] private float maxMouseDistance = 2;
        protected override bool Applicable
        {
            get
            {
                if (startPos.HasValue && SelectionContext.FirstOrNullHovered != null && SelectionContext.FirstOrNullHovered.GetExtension<ISelectable>() != null)
                    return Vector2.Distance(startPos.Value, Input.mousePosition) < maxMouseDistance && SelectionContext.FirstOrNullHovered != null;
                return false;
            }
        }
        public override int Priority => 0;
        private Vector2? startPos;
        public override void InputStart()
        {
            startPos = Input.mousePosition;
        }
        public override void InputStop()
        {
            if (Applicable)
            {
                SelectionContext.Select(SelectionContext.FirstOrNullHovered.GetExtension<ISelectable>());
            }
        }
    }
}
