using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTSEngine.Core
{   
    [RequireComponent(typeof(Collider))]
    public class RTSBehaviour : MonoBehaviour
    {
        public RTSEntity entity;

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
        private Dictionary<Type, IExtension> extensions = new Dictionary<Type, IExtension>();

        public T GetExtension<T>() where T : IExtension
        {
            if (extensions.TryGetValue(typeof(T), out IExtension extension))
                return (T)extension;
            return default;
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
        private void OnEnable()
        {
            extensions = entity.Build(this);
            EntityContext.Register(this);
        }

        protected virtual void OnExitSceneImpl()
        {
            foreach (IExtension extension in extensions.Values)
                extension.ExitScene();
            EntityContext.hovered.Remove(this);
            EntityContext.Unregister(this);
            GetComponent<Collider>().enabled = false;
        }
    }
}
