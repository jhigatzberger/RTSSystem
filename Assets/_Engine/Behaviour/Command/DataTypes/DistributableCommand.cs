using System;

namespace JHiga.RTSEngine.CommandPattern
{
    /// <summary>
    /// Packed by <see cref="CommandDistributor.DistributableCommandFromSelectionContext(CommandProperties, bool)"/>.
    /// Carries all information needed to enqueue a command on each client.
    /// </summary>
    public struct DistributableCommand
    {
        public CompiledCommand data;
        public int[] entities;
        public bool clearQueueOnEnqeue;
    }
}
