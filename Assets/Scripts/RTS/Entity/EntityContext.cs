using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RTS.Entity
{
    public static class EntityContext
    {
        #region Registering
        public static Dictionary<int, BaseEntity> entities = new Dictionary<int, BaseEntity>();
        private static int id = 0;
        public static int Register(BaseEntity entity)
        {
            entities.Add(++id, entity);
            return id;
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
