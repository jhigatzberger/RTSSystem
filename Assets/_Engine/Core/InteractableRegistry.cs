using System.Collections.Generic;
using System.Linq;
using System;

namespace JHiga.RTSEngine
{
    public static class InteractableRegistry
    {
        #region Registering
        public static Dictionary<int, IExtendableInteractable> entities = new Dictionary<int, IExtendableInteractable>();
        public static event Action<int> OnRequireEntityID;
        public static void RequireEntityID(int spawnID)
        {
            OnRequireEntityID?.Invoke(spawnID);
        }
        public static void Register(IExtendableInteractable entity)
        {
            entities.Add(entity.EntityId, entity);
        }
        public static void Unregister(IExtendableInteractable entity)
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
