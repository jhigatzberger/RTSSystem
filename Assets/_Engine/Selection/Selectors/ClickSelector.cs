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
                if (startPos.HasValue && EntityManager.FirstOrNullHovered != null && EntityManager.FirstOrNullHovered.GetScriptableComponent<ISelectable>() != null)
                    return Vector2.Distance(startPos.Value, Input.mousePosition) < maxMouseDistance && EntityManager.hovered.Count > 0;
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
                print(EntityManager.FirstOrNullHovered.name);
                SelectionContext.Select(EntityManager.FirstOrNullHovered.GetScriptableComponent<ISelectable>());
            }
        }
    }

}
