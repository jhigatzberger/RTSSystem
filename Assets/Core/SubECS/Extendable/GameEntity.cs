using UnityEngine;
using System;

namespace JHiga.RTSEngine
{
    /// <summary>
    /// Any "Actor" in your RTS Game. Can be a tree that should be fellable, a unit, a building...
    /// Gets spawned by <see cref="GameEntityFactory"/>.
    /// </summary>
    public class GameEntity : MonoBehaviour, IExtendableEntity
    {
        public static GameEntity Get(UID uid)
        {
            return GameEntityPool.Get(uid).Entities[uid.entityIndex];
        }
        public static GameEntity Get(int uid)
        {
            return GameEntityPool.Get(uid).Entities[UID.GetEntityIndex(uid)];
        }
        [SerializeField] private UID _id;
        public UID UniqueID
        {
            get => _id;
            internal set
            {
                _id = value;
                gameObject.layer = LayerMask.NameToLayer(PlayerContext.players[value.playerIndex].layerName);
                foreach (IEntityExtension extension in Extensions)
                    extension.Enable();
            }
        }
        public IEntityExtension[] Extensions { get; set; }
        public MonoBehaviour MonoBehaviour => this;
        public T GetExtension<T>() where T : IEntityExtension
        {
            foreach (IEntityExtension x in Extensions)
                if (x is T t)
                    return t;
            return default;
        }
        public bool TryGetExtension<T>(out T extension) where T : IEntityExtension
        {
            extension = default;
            foreach (IEntityExtension x in Extensions)
                if (x is T t)
                {
                    extension = t;
                    return true;
                }
            return false;
        }
        public void Clear()
        {
            foreach (IEntityExtension extension in Extensions)
                extension.Clear();
        }
        private void OnDisable()
        {
            foreach (IEntityExtension extension in Extensions)
                extension.Disable();
        }
    }
}
