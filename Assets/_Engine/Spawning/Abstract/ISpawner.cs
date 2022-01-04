namespace JHiga.RTSEngine.Spawning
{
    public interface ISpawner : IExtension
    {
        public void AuthorizeID(int id);
        public void Enqueue(PooledEntityFactory toSpawn, float time);
    }
}

