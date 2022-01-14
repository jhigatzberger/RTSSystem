using UnityEngine;
using System;

namespace JHiga.RTSEngine
{
    /// <summary>
    /// A pooled implementation of <see cref="IExtendable"/>.
    /// Gets spawned by <see cref="PooledGameEntityFactory"/>.
    /// </summary>
    public class GameEntity : MonoBehaviour, IExtendable
    {
        #region Initialization
        [SerializeField] private UID _id;
        public UID EntityId
        {
            get => _id;
            internal set
            {
                _id = value;
                gameObject.layer = LayerMask.NameToLayer(PlayerContext.players[value.playerIndex].layerName);
                foreach (IInteractableExtension extension in Extensions)
                    extension.Enable();
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
        #region Clean Up
        private void OnDisable()
        {
            foreach (IInteractableExtension extension in Extensions)
                extension.Disable();
        }
        public void Clear()
        {
            foreach (IInteractableExtension extension in Extensions)
                extension.Clear();
        }
        #endregion
    }
}
