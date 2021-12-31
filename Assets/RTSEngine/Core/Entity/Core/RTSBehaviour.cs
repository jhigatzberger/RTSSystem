using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTSEngine.Core
{
    [RequireComponent(typeof(Collider))]
    public class RTSBehaviour : MonoBehaviour
    {
        #region Initialization
        private RTSEntity _entity;
        [SerializeField] private int _id;
        public void Spawn(RTSEntity entity, int id, int team)
        {
            _entity = entity;
            if(extensions == null)
                extensions = entity.Build(this);
            Id = id;
            Team = team;
        }
        private bool _registered;
        public bool Registered
        {
            get => _registered;
            private set
            {
                if(value != _registered)
                {
                    if (value)
                        EntityContext.Register(this);
                    else
                        EntityContext.Unregister(this);
                    _registered = value;
                }
            }
        }
        public int Id
        {
            get => _id;
            internal set
            {
                if (Registered)
                    Registered = false;
                _id = value;
                if (value >= 0)
                    Registered = true;
            }
        }
        #endregion
        #region Team
        [SerializeField] private int _team = -1;
        public int Team { get => _team;
            set
            {
                if(_team != value)
                {
                    _team = value;
                    gameObject.layer = LayerMask.NameToLayer(RTSEngine.Team.TeamContext.teams[value].layerName);
                }
            }
        }
        #endregion
        #region EntityExtension
        private IExtension[] extensions;
        public T GetExtension<T>() where T : IExtension
        {
            foreach (IExtension x in extensions)
                if (x is T t)
                    return t;
            return default;
        }
        public bool TryGetExtension<T>(out T extension) where T : IExtension
        {
            extension = default;
            foreach (IExtension x in extensions)
                if (x is T t)
                {
                    extension = t;
                    return true;
                }
            return false;
        }
        #endregion
        #region Hovering
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
        #endregion
        #region Clean Up
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
        protected virtual void OnExitSceneImpl()
        {
            foreach (IExtension extension in extensions)
                extension.ExitScene();
            EntityContext.hovered.Remove(this);
            GetComponent<Collider>().enabled = false;
            Id = -1;
            _entity.Return(this);
        }
        #endregion
    }
}
