using JHiga.RTSEngine.CommandPattern;

namespace JHiga.RTSEngine.Network
{
    public struct ScheduledCommand
    {
        public SkinnedCommand command;
        public ulong scheduledStep;
    }
}