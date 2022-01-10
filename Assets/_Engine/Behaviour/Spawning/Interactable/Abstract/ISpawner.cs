namespace JHiga.RTSEngine.Spawning
{
    public interface ISpawner : IInteractableExtension
    {
        public void AuthorizeID(int id);
        public void Enqueue(PooledGameEntityFactory toSpawn, float time);
    }
}

