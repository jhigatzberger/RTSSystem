using UnityEngine;
using System;
using System.Collections.Generic;

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
            return GameEntityPool.Get(uid).entities[uid.entityIndex];
        }
        public static GameEntity Get(int uid)
        {
            return GameEntityPool.Get(uid).entities[UID.GetEntityIndex(uid)];
        }
        [SerializeField] private UID _id;
        public UID UniqueID
        {
            get => _id;
            internal set
            {
                _id = value;
                gameObject.layer = PlayerContext.players[value.playerIndex].ownMask;

                if (TryGetComponent(out Collider coll))
                    coll.enabled = true;
                foreach (IEntityExtension extension in Extensions)
                    extension.Enable();
            }
        }
        public MonoBehaviour MonoBehaviour => this;
        public Dictionary<Type, int> extensionMap;
        public IEntityExtension[] Extensions { get; set; }
        public T GetExtension<T>() where T : IEntityExtension
        {
            return (T)Extensions[extensionMap[typeof(T)]];
        }
        public bool TryGetExtension<T>(out T extension) where T : IEntityExtension
        {
            extension = default;
            if (extensionMap.TryGetValue(typeof(T), out int x))
            {
                extension = (T)Extensions[x];
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
            if(TryGetComponent(out Collider coll))
                coll.enabled = false;
            foreach (IEntityExtension extension in Extensions)
                extension.Disable();
        }

        public void Disable()
        {
            enabled = false;
        }
    }
}
