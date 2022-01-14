using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace JHiga.RTSEngine
{
    public static class EntityConstants
    {
        public const int MAX_POOL_SIZE = 999;
        public const int MAX_POOLS = 999;

        public static event Action<int> OnRequireEntityID;
        public static void RequireEntityID(int spawnerId)
        {
            OnRequireEntityID?.Invoke(spawnerId);
        }
        public static IExtendable FindEntityByUniqueId(UID uniqueId)
        {
            Debug.Log(uniqueId);

            return PlayerContext.players[uniqueId.playerIndex].Factories[uniqueId.poolIndex].Get(uniqueId.entityIndex);
        }
    }
}
