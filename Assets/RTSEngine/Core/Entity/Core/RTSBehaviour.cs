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
        [SerializeField] private int _team;
        public int Team { get => _team; set => _team = value;  } 
        public event Action OnClear;

        private IExtension[] extensions;
        public T GetExtension<T>() where T : IExtension
        {
            foreach(IExtension extension in extensions)
                if(extension is T t)
                    return t;
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
            foreach (IExtension extension in extensions)
                extension.ExitScene();
            EntityContext.hovered.Remove(this);
            EntityContext.Unregister(this);
            GetComponent<Collider>().enabled = false;
        }
    }
}
