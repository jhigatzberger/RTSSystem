using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.Spawning
{
    public class SpawnerExtension : RTSExtension, ISpawner
    {
        private Queue<EntitySpawnData> toSpawn = new Queue<EntitySpawnData>();
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
            if (toSpawn.Count == 0 || next != null || nextID != -1)
                return;
            next = toSpawn.Dequeue();
            EntityContext.RequireEntityID(Behaviour.id);
            timeStamp = LockStep.time;
        }

        private void DoSpawn()
        {
            if (nextID < 0 || !next.HasValue || next.Value.time > LockStep.time - timeStamp)
                return;
            RTSBehaviour entity = Object.Instantiate(next.Value.prefab, Behaviour.transform.position, Behaviour.transform.rotation).GetComponent<RTSBehaviour>(); // pooling!
            entity.id = nextID;
            entity.Team = Behaviour.Team;
            entity.gameObject.layer = Behaviour.gameObject.layer;
            next = null;
            nextID = -1;
            EntityContext.Register(entity);
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
        public void Enqueue(GameObject gameObject, float spawnTime)
        {
            toSpawn.Enqueue(new EntitySpawnData
            {
                prefab = gameObject,
                time = spawnTime
            });
        }
    }

    public struct EntitySpawnData
    {
        public float time;
        public GameObject prefab;
    }
}
