using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTS.Entity
{   
    public abstract class BaseEntity : MonoBehaviour
    {
        public int id;
        public abstract int Priority { get; }
        public event Action<bool> OnSelectedUpdate;

        public int _selectionPosition = -1;
        public int SelectionPosition
        {
            get
            {
                return _selectionPosition;
            }
            set
            {
                if (Selected && value<0 || !Selected && value>=0)
                {
                    _selectionPosition = value;
                    OnSelectedUpdate?.Invoke(Selected);
                }
            }
        }
        public bool Selected
        {
            get
            {
                return SelectionPosition>=0;
            }
        }
        private void Start()
        {
            id = EntityContext.Register(this);
        }
        private void OnMouseEnter()
        {
            EntityContext.hovered.Add(this);
        }
        private void OnMouseExit()
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
        public delegate void ExitScene();
        public event ExitScene OnExitScene;
        protected virtual void OnExitSceneImpl()
        {
            OnExitScene?.Invoke();
            EntityContext.hovered.Remove(this);
        }
    }
}
