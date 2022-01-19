using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public class SpawnerExtension : BaseInteractableExtension<SpawnerProperties>, ISpawner
    {
        private Queue<EntitySpawnData> spawnQueue = new Queue<EntitySpawnData>();
        private float timeStamp;
        public Target Waypoint {
            get
            {
                return new Target
                {
                    position = Properties.doorPosition
                };
            }
            set => throw new System.NotImplementedException();
        }
        public SpawnerExtension(IExtendableEntity entity, SpawnerProperties properties) : base(entity, properties)
        {
            LockStep.OnStep += LockStep_OnStep;
        }
        public override void Disable()
        {
            LockStep.OnStep -= LockStep_OnStep;
        }
        private void LockStep_OnStep()
        {
            Spawn();
        }
        private void Spawn()
        {
            if (spawnQueue.Count==0 || spawnQueue.Peek().time > LockStep.time - timeStamp)
                return;

            EntitySpawnData spawnData = spawnQueue.Dequeue();
            spawnData.factory.Spawn(Entity.MonoBehaviour.transform.position + Properties.doorPosition, spawnData.uid);
        }
        public void Enqueue(UID uid, float spawnTime)
        {
            Debug.Log("Enqueue spawn " + uid.uniqueId);
            spawnQueue.Enqueue(new EntitySpawnData
            {
                factory = GameEntityPool.Get(uid),
                uid = uid,
                time = spawnTime
            });
        }
    }
    public struct EntitySpawnData
    {
        public float time;
        public UID uid;
        public GameEntityPool factory;
    }
}
