using UnityEngine;
using System;
using JHiga.RTSEngine.Team;

namespace JHiga.RTSEngine
{
    [RequireComponent(typeof(Collider))]
    public class GameEntity : MonoBehaviour, IExtendable
    {
        #region Initialization
        private Action<GameEntity> _returnAction;
        [SerializeField] private int _id;
        public void Spawn(Action<GameEntity> returnAction, int entityId, int playerId)
        {
            _returnAction = returnAction;
            EntityId = entityId;
            PlayerId = playerId;
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
                        EntityManager.Register(this);
                    else
                        EntityManager.Unregister(this);
                    _registered = value;
                }
            }
        }
        public int EntityId
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
        public int PlayerId { get => _team;
            set
            {
                if(_team != value)
                {
                    _team = value;
                    gameObject.layer = LayerMask.NameToLayer(TeamContext.teams[value].layerName);
                }
            }
        }
        #endregion
        #region Components
        public IExtension[] ScriptableComponents { get; set; }
        public MonoBehaviour MonoBehaviour => this;
        public T GetScriptableComponent<T>() where T : IExtension
        {
            foreach (IExtension x in ScriptableComponents)
                if (x is T t)
                    return t;
            return default;
        }
        public bool TryGetScriptableComponent<T>(out T extension) where T : IExtension
        {
            extension = default;
            foreach (IExtension x in ScriptableComponents)
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
                EntityManager.hovered.Add(this);
        }
        private void OnMouseExit()
        {
            if (enabled)
                EntityManager.hovered.Remove(this);
        }
        #endregion
        #region Clean Up
        private void OnDisable()
        {
            foreach (IExtension extension in ScriptableComponents)
                extension.Disable();
            EntityManager.hovered.Remove(this);
            GetComponent<Collider>().enabled = false;
            EntityId = -1;
            _returnAction(this);
        }
        public void Clear()
        {
            foreach (IExtension extension in ScriptableComponents)
                extension.Clear();
        }
        #endregion
    }
}
