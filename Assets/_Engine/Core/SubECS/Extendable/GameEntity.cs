using UnityEngine;
using System;

namespace JHiga.RTSEngine
{
    /// <summary>
    /// A pooled implementation of <see cref="IExtendableInteractable"/>.
    /// Gets spawned by <see cref="PooledGameEntityFactory"/>.
    /// Dependant on <see cref="InteractableRegistry"/> for seamless <see cref="InteractableRegistry.Register(IExtendableInteractable)"/> and <see cref="InteractableRegistry.hovered"/> connections.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class GameEntity : MonoBehaviour, IExtendableInteractable
    {
        #region Initialization
        private Action<GameEntity> _returnAction;
        [SerializeField] private int _id;
        public void Spawn(Action<GameEntity> returnAction, int entityId, int playerId)
        {
            print(entityId + " " + playerId);
            _returnAction = returnAction;
            EntityId = entityId;
            PlayerId = playerId;
            foreach (IInteractableExtension extension in Extensions)
                extension.Enable();
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
                        InteractableRegistry.Register(this);
                    else
                        InteractableRegistry.Unregister(this);
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
                    gameObject.layer = LayerMask.NameToLayer(PlayerContext.players[value].layerName);
                }
            }
        }
        #endregion
        #region Components
        public IInteractableExtension[] Extensions { get; set; }
        public MonoBehaviour MonoBehaviour => this;
        public T GetScriptableComponent<T>() where T : IInteractableExtension
        {
            foreach (IInteractableExtension x in Extensions)
                if (x is T t)
                    return t;
            return default;
        }
        public bool TryGetScriptableComponent<T>(out T extension) where T : IInteractableExtension
        {
            extension = default;
            foreach (IInteractableExtension x in Extensions)
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
                InteractableRegistry.hovered.Add(this);
        }
        private void OnMouseExit()
        {
            if (enabled)
                InteractableRegistry.hovered.Remove(this);
        }
        #endregion
        #region Clean Up
        private void OnDisable()
        {
            foreach (IInteractableExtension extension in Extensions)
                extension.Disable();
            InteractableRegistry.hovered.Remove(this);
            GetComponent<Collider>().enabled = false;
            EntityId = -1;
            _returnAction(this);
        }
        public void Clear()
        {
            foreach (IInteractableExtension extension in Extensions)
                extension.Clear();
        }
        #endregion
    }
}
