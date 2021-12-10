using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTS
{   
    public abstract class EntityController : MonoBehaviour
    {
        public abstract int Priority { get; }
        public event Action<Selection> OnSelection;
        private Selection _selection;
        public Selection Selection
        {
            get
            {
                return _selection;
            }
            set
            {
                if (_selection != value)
                {
                    _selection = value;
                    OnSelection?.Invoke(value);
                }
            }
        }
        public void OnMouseOverSelectableObject()
        {
            Context.hoveredEntities.Add(this);
        }
        public void OnMouseExitSelectableObject()
        {
            Context.hoveredEntities.Remove(this);
        }
        private void OnDestroy()
        {
            OnExitScene();
        }
        private void OnDisable()
        {
            OnExitScene();
        }
        protected virtual void OnExitScene()
        {
            Context.hoveredEntities.Remove(this);
        }
    }
}
