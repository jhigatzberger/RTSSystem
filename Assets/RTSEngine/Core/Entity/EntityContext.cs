using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RTSEngine.Core
{
    public static class EntityContext
    {
        #region Registering
        public static Dictionary<int, RTSBehaviour> entities = new Dictionary<int, RTSBehaviour>();
        public static event Action<int> OnRequireEntityID;
        public static void RequireEntityID(int spawnID)
        {
            OnRequireEntityID?.Invoke(spawnID);
        }
        public static void Register(RTSBehaviour entity)
        {
            entities.Add(entity.id, entity);
        }
        public static void Unregister(RTSBehaviour entity)
        {
            entities.Remove(entity.id);
        }
        #endregion
        #region Hovering
        public static HashSet<RTSBehaviour> hovered = new HashSet<RTSBehaviour>();
        public static RTSBehaviour FirstOrNullHovered
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
