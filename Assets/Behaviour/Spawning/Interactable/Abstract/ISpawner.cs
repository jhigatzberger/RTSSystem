using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public interface ISpawner : IEntityExtension
    {
        public Vector3 DefaultSpawnOffset { get; }
        public Vector3 SpawnOffset { get; set; }
        public void Enqueue(UID uid, float time);
        public int QueueSize { get; }
    }
}

