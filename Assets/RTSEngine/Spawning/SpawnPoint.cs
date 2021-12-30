using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity
{
    public class SpawnPoint : MonoBehaviour, ISpawner
    {
        private Queue<EntitySpawnData> toSpawn = new Queue<EntitySpawnData>();
        private EntitySpawnData? next;
        private float timeStamp;

        public BaseEntity Entity { get; set; }
        private int nextID = -1;

        public void AuthorizeID(int id)
        {
            nextID = id;
        }

        public void RequestNext()
        {
            if (toSpawn.Count == 0 || next != null || nextID != -1)
                return;
            next = toSpawn.Dequeue();
            EntityContext.RequireEntityID(Entity.id);
            timeStamp = LockStep.time;
        }

        private void DoSpawn()
        {
            if (nextID < 0 || !next.HasValue || next.Value.time > LockStep.time - timeStamp)
                return;
            BaseEntity entity = Instantiate(next.Value.prefab, transform.position, transform.rotation).GetComponent<BaseEntity>();
            entity.id = nextID;
            entity.Team = Entity.Team;
            entity.gameObject.layer = gameObject.layer;
            next = null;
            nextID = -1;
            EntityContext.Register(entity);
        }

        private void Start()
        {
            EntityContext.Register(Entity);
            LockStep.OnStep += LockStep_OnStep;
        }

        private void LockStep_OnStep()
        {
            RequestNext();
            DoSpawn();
        }

        public void OnExitScene()
        {
            LockStep.OnStep -= LockStep_OnStep;
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
