using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTS
{   
    public abstract class BaseEntity : MonoBehaviour
    {
        public abstract int Priority { get; }
        public event Action<bool> OnSelectedUpdate;
        private bool _selected;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnSelectedUpdate?.Invoke(value);
                }
            }
        }
        public void OnMouseOverSelectableObject()
        {
            EntityContext.hovered.Add(this);
        }
        public void OnMouseExitSelectableObject()
        {
            EntityContext.hovered.Remove(this);
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
            // POOLING RETURN
            EntityContext.hovered.Remove(this);
        }
    }
}
