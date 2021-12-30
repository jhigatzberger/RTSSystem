using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTSEngine.Entity
{   
    public class BaseEntity : MonoBehaviour
    {
        public int id;
        public int _team;
        public int Team
        {
            get => _team;
            set
            {
                if(value != _team)
                    _team = value;
            }
        }
        public virtual int Priority { get; }
        public event Action<bool> OnSelectedUpdate;
        private IEntityExtension[] extensions;

        private int _selectionPosition = -1;
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
        private void OnMouseEnter()
        {
            if(enabled)
                EntityContext.hovered.Add(this);
        }
        private void OnMouseExit()
        {
            if (enabled)
                EntityContext.hovered.Remove(this);
        }
        private void OnDestroy()
        {
            OnExitSceneImpl();
        }
        private void OnDisable()
        {
            OnExitSceneImpl();
        }
        public event Action OnClear;
        public virtual void Clear()
        {
            OnClear?.Invoke();
        }

        private void Awake()
        {
            extensions = GetComponents<IEntityExtension>();
            foreach (IEntityExtension entityExtension in extensions)
                entityExtension.Entity = this;
        }

        protected virtual void OnExitSceneImpl()
        {
            foreach (IEntityExtension entityExtension in extensions)
                entityExtension.OnExitScene();
            EntityContext.hovered.Remove(this);
            Selection.Context.Deselect(this);
            EntityContext.Unregister(this);
        }
    }
}
