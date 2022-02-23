using System;
using System.Collections.Generic;

namespace JHiga.RTSEngine.Spawning
{
    public static class SpawnEvents
    {
        private static Dictionary<int, Action<IExtendableEntity>> callbacks = new Dictionary<int, Action<IExtendableEntity>>();
        public static event Action<SpawnRequest> OnRequestSpawn;
        public static void RequestSpawn(SpawnRequest spawnRequest, Action<IExtendableEntity> callback = null)
        {
            if (callback != null)
                callbacks.Add(spawnRequest.spawnerUID, callback);
            OnRequestSpawn?.Invoke(spawnRequest);
        }
        public static void TriggerSpawnCallback(int uid, IExtendableEntity entity)
        {
            if (callbacks.TryGetValue(uid, out Action<IExtendableEntity> action))
            {
                action(entity);
                callbacks.Remove(uid);
            }
        }
    }
    public struct SpawnRequest
    {
        public int spawnerUID;
        public int poolIndex;
        public float time;
    }
    public struct SpawnData
    {
        public int entityUID;
        public int spawnerUID;
        public float time;
    }
}
