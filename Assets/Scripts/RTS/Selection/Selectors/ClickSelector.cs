using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
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
                    return Vector2.Distance(startPos.Value, Input.mousePosition) < maxMouseDistance;
                return false;
            }
        }

        public override int Prority
        {
            get
            {
                return 0;
            }
        }

        private Vector2? startPos;
        public override void InputStart()
        {
            startPos = Input.mousePosition;
        }

        public override void InputStop()
        {
            if (Applicable)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.MaxValue, Manager.selectableLayerMask))
                {
                    SelectableObject selectableObject = hit.collider.GetComponent<SelectableObject>();
                    if(selectableObject != null)
                        Manager.Selection.Add(selectableObject.controller);
                }
            }
        }
    }
}