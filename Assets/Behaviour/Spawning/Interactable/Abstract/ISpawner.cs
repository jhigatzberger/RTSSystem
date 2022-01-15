namespace JHiga.RTSEngine.Spawning
{
    public interface ISpawner : IEntityExtension
    {
        public Target Waypoint { get; set; }
        public void Enqueue(UID uid, float time);
    }
}

