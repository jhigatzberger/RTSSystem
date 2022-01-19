using System;

namespace JHiga.RTSEngine.Spawning
{
    public static class SpawnEvents
    {
        public static event Action<SpawnRequest> OnRequestSpawn;
        public static void RequestSpawn(SpawnRequest spawnerUID)
        {
            OnRequestSpawn?.Invoke(spawnerUID);
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