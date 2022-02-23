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
        private Collider coll;
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

                if (TryGetComponent(out coll))
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
            if(coll != null)
                coll.enabled = false;
            foreach (IEntityExtension extension in Extensions)
                extension.Disable();
        }
        public void Disable(bool setGameObjectInactive)
        {
            enabled = false;
            if (setGameObjectInactive)
                gameObject.SetActive(false);
        }
        public Vector3 Position => transform.position;

        public bool IsActivePlayer => PlayerContext.PlayerId == UniqueID.playerIndex;

        public Vector3 ClosestEdgePoint(Vector3 pos)
        {
            if (coll == null)
                return Position;
            return coll.ClosestPointOnBounds(pos);
        }
    }
}
