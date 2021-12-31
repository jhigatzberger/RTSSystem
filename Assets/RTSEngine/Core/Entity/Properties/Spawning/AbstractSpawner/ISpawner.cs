using UnityEngine;

namespace RTSEngine.Core.Spawning
{
    public interface ISpawner : IExtension
    {
        public void AuthorizeID(int id);
        public void Enqueue(RTSEntity toSpawn, float time);
    }
}

