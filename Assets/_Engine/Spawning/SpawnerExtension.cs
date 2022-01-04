using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public class SpawnerExtension : Extension, ISpawner
    {
        private Queue<EntitySpawnData> spawnQueue = new Queue<EntitySpawnData>();
        private EntitySpawnData? next;
        private float timeStamp;
        private int nextID = -1;

        public SpawnerExtension(IExtendable entity ) : base(entity)
        {
            LockStep.OnStep += LockStep_OnStep;
        }

        public void AuthorizeID(int id)
        {
            nextID = id;
        }

        public void RequestNext()
        {
            if (spawnQueue.Count == 0 || next != null || nextID != -1)
                return;
            next = spawnQueue.Dequeue();
            EntityManager.RequireEntityID(Extendable.EntityId);
            timeStamp = LockStep.time;
        }

        private void DoSpawn()
        {
            if (nextID < 0 || !next.HasValue || next.Value.time > LockStep.time - timeStamp)
                return;
            next.Value.toSpawn.Spawn(Extendable.MonoBehaviour.transform.position, nextID, Extendable.PlayerId);
            next = null;
            nextID = -1;
        }
        public override void Disable()
        {
            LockStep.OnStep -= LockStep_OnStep;
        }

        private void LockStep_OnStep()
        {
            RequestNext();
            DoSpawn();
        }
        public void Enqueue(PooledEntityFactory toSpawn, float spawnTime)
        {
            spawnQueue.Enqueue(new EntitySpawnData
            {
                toSpawn = toSpawn,
                time = spawnTime
            });
        }
    }

    public struct EntitySpawnData
    {
        public float time;
        public PooledEntityFactory toSpawn;
    }
}
