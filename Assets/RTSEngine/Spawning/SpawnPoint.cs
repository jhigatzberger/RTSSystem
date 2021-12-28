using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity
{
    public class SpawnPoint : MonoBehaviour, ISpawner
    {

        private int nextID = -1;
        private GameObject toSpawn;

        public void AuthorizeID(int id)
        {
            nextID = id;
        }

        public void Spawn(GameObject gameObject)
        {
            toSpawn = gameObject;
            EntityContext.RequireEntityID(Entity.id);
        }

        private void DoSpawn()
        {
            if (nextID < 0 || toSpawn == null)
                return;
            BaseEntity entity = Instantiate(toSpawn, transform.position, transform.rotation).GetComponent<BaseEntity>();
            entity.id = nextID;
            entity.team = Entity.team;
            toSpawn = null;
            nextID = -1;
            EntityContext.Register(entity);
        }

        public GameObject spawnObject;
        public CommandData onSpawn;
        public float spawnDelay;
        private float lastSpawn = -5;

        private BaseEntity _entity;
        public BaseEntity Entity => _entity;

        private void Start()
        {
            _entity = GetComponent<BaseEntity>();
            EntityContext.Register(Entity);
            LockStep.OnStep += StepSpawn;
        }

        public void StepSpawn()
        {
            DoSpawn();
            if(LockStep.time - lastSpawn > spawnDelay)
            {
                lastSpawn = LockStep.time;
                Spawn(spawnObject);
            }
        }

        public void OnExitScene()
        {
            LockStep.OnStep -= StepSpawn;
        }
    }

}
