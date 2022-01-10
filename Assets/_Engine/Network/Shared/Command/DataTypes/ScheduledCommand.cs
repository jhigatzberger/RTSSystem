using JHiga.RTSEngine.CommandPattern;

namespace JHiga.RTSEngine.Network
{
    public struct ScheduledCommand
    {
        public DistributableCommand command;
        public ulong scheduledStep;
    }
}