using System;

namespace RTSEngine.Core.InputHandling
{
    public struct DistributedCommand : IEquatable<DistributedCommand>
    {
        public CommandData data;
        public int entity;
        public bool clearQueueOnEnqeue;

        public bool Equals(DistributedCommand other)
        {
            return other.data.Equals(data) && entity == other.entity;
        }
    }
}
