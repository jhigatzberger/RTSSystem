namespace JHiga.RTSEngine.Spawning
{
    public interface ISpawner : IInteractableExtension
    {
        public Target Waypoint { get; set; }
        public void AuthorizeID(int id);
        public void Enqueue(int entityType, float time);
    }
}

