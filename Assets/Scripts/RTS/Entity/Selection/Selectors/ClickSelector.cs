using RTS.Entity.Selection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.Selection.Selectors
{
    public class ClickSelector : Selector
    {
        [Tooltip("default value is the exact inverse of the boxselect threshhold")]
        [SerializeField] private float maxMouseDistance = 2;
        protected override bool Applicable
        {
            get
            {
                if (startPos.HasValue)
                    return Vector2.Distance(startPos.Value, Input.mousePosition) < maxMouseDistance && EntityContext.hovered.Count > 0;
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
                BaseEntity _entity = null;
                foreach (BaseEntity entity in EntityContext.hovered)
                    if (_entity == null || _entity.Priority < entity.Priority)
                        _entity = entity;
                Context.Select(_entity);
            }
        }
    }
}