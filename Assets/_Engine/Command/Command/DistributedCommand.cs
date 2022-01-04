using System;

namespace JHiga.RTSEngine.CommandPattern
{
    public struct DistributedCommand
    {
        public CommandData data;
        public int[] entities;
        public bool clearQueueOnEnqeue;
    }
}
