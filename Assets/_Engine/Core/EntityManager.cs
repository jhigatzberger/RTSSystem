using System.Collections.Generic;
using System.Linq;
using System;

namespace JHiga.RTSEngine
{
    public static class EntityManager
    {
        #region Registering
        public static Dictionary<int, PooledEntity> entities = new Dictionary<int, PooledEntity>();
        public static event Action<int> OnRequireEntityID;
        public static void RequireEntityID(int spawnID)
        {
            OnRequireEntityID?.Invoke(spawnID);
        }
        public static void Register(PooledEntity entity)
        {
            entities.Add(entity.EntityId, entity);
        }
        public static void Unregister(PooledEntity entity)
        {
            entities.Remove(entity.EntityId);
        }
        #endregion
        #region Hovering
        public static HashSet<PooledEntity> hovered = new HashSet<PooledEntity>();
        public static PooledEntity FirstOrNullHovered
        {
            get
            {
                if (hovered.Count < 1)
                    return null;
                else
                    return hovered.First();
            }
        }
        #endregion
    }
}
