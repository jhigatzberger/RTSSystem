using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.Spawning
{
    public class SpawnerExtension : RTSExtension, ISpawner
    {
        private Queue<EntitySpawnData> spawnQueue = new Queue<EntitySpawnData>();
        private EntitySpawnData? next;
        private float timeStamp;
        private int nextID = -1;

        public SpawnerExtension(RTSBehaviour behaviour) : base(behaviour)
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
            EntityContext.RequireEntityID(Behaviour.Id);
            timeStamp = LockStep.time;
        }

        private void DoSpawn()
        {
            if (nextID < 0 || !next.HasValue || next.Value.time > LockStep.time - timeStamp)
                return;
            next.Value.toSpawn.Instantiate(Behaviour.transform.position, nextID, Behaviour.Team);
            next = null;
            nextID = -1;
        }
        protected override void OnExitScene()
        {
            LockStep.OnStep -= LockStep_OnStep;
        }

        private void LockStep_OnStep()
        {
            RequestNext();
            DoSpawn();
        }
        public void Enqueue(RTSEntity toSpawn, float spawnTime)
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
        public RTSEntity toSpawn;
    }
}
