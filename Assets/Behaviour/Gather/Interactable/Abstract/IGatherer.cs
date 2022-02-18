
namespace JHiga.RTSEngine.Gather
{
    public interface IGatherer : IEntityExtension
    {
        public IGatherable Target { get; }
        public IDropoff Dropoff { get; }
        public float Speed { get; }
        public int Capacity { get; }
        public int CurrentResourceType { get; }
        public int CurrentLoad { get; }
        public void Gather();
        public void DropOff();
    }
}