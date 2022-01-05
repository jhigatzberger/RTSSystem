using System.Collections.Generic;
using System.Linq;
using System;

namespace JHiga.RTSEngine
{
    public static class EntityManager
    {
        #region Registering
        public static Dictionary<int, GameEntity> entities = new Dictionary<int, GameEntity>();
        public static event Action<int> OnRequireEntityID;
        public static void RequireEntityID(int spawnID)
        {
            OnRequireEntityID?.Invoke(spawnID);
        }
        public static void Register(GameEntity entity)
        {
            entities.Add(entity.EntityId, entity);
        }
        public static void Unregister(GameEntity entity)
        {
            entities.Remove(entity.EntityId);
        }
        #endregion
        #region Hovering
        public static HashSet<GameEntity> hovered = new HashSet<GameEntity>();
        public static GameEntity FirstOrNullHovered
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
