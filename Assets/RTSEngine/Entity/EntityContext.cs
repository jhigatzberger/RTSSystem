using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RTSEngine.Entity
{
    public static class EntityContext
    {
        #region Registering
        public static Dictionary<int, BaseEntity> entities = new Dictionary<int, BaseEntity>();
        public static event Action<int> OnRequireEntityID;
        public static void RequireEntityID(int spawnID)
        {
            OnRequireEntityID?.Invoke(spawnID);
        }
        public static void Register(BaseEntity entity)
        {
            entities.Add(entity.id, entity);
        }
        public static void Unregister(BaseEntity entity)
        {
            entities.Remove(entity.id);
        }
        #endregion
        #region Hovering
        public static HashSet<BaseEntity> hovered = new HashSet<BaseEntity>();
        public static BaseEntity FirstOrNullHovered
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
