namespace JHiga.RTSEngine.Gather
{
    public interface IGatherable : IEntityExtension
    {
        public enum StorageType
        {
            Finite,
            Infinite
        }
        public StorageType GatherableStorageType { get; }
        public int TotalStorage { get; }
        public int Rate { get; }
        public int ResourceType { get; }
        public int Gather(float speed);
    }
}
