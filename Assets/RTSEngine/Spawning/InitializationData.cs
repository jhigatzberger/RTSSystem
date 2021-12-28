using System;

namespace RTSEngine.Entity
{
    public struct InitializationData : IEquatable<InitializationData>
    {
        public int entityID;
        public int spawnID;
        public bool Equals(InitializationData other)
        {
            return other.entityID == entityID && other.spawnID == spawnID;
        }
    }
}