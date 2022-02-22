using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public class SpawnerExtension : BaseInteractableExtension<SpawnerProperties>, ISpawner
    {
        private Queue<EntitySpawnData> spawnQueue = new Queue<EntitySpawnData>();
        private float timeStamp;
        public Vector3 SpawnOffset { get; set; }

        public int QueueSize => spawnQueue.Count;

        public Vector3 DefaultSpawnOffset { get => Properties.offset + Entity.MonoBehaviour.transform.position; }

        private IProgressIndicator progressIndicator;

        public SpawnerExtension(IExtendableEntity entity, SpawnerProperties properties) : base(entity, properties)
        {
            if(Properties.progressIndicator != null)
            {
                progressIndicator = Object.Instantiate(Properties.progressIndicator, Entity.MonoBehaviour.transform).GetComponent<IProgressIndicator>();
            }
        }
        public override void Enable()
        {
            SpawnOffset = DefaultSpawnOffset;
            LockStep.OnStep += LockStep_OnStep;
        }

        public override void Disable()
        {
            LockStep.OnStep -= LockStep_OnStep;
        }
        private void LockStep_OnStep()
        {
            if (progressIndicator != null)
                progressIndicator.Hide(spawnQueue.Count == 0);
            if (spawnQueue.Count == 0)
                return;
            EntitySpawnData data = spawnQueue.Peek();
            if (progressIndicator != null)
            {
                progressIndicator.SetProgress((LockStep.time - timeStamp) / data.spawnTime);
            }
            if( timeStamp + data.spawnTime > LockStep.time )
                return;
            Spawn();
        }
        private void Spawn()
        {
            EntitySpawnData spawnData = spawnQueue.Dequeue();
            spawnData.factory.Spawn(SpawnOffset, spawnData.uid);
            timeStamp = LockStep.time;
        }
        public void Enqueue(UID uid, float spawnTime)
        {
            if (spawnQueue.Count == 0)
                timeStamp = LockStep.time;
            spawnQueue.Enqueue(new EntitySpawnData
            {
                factory = GameEntityPool.Get(uid),
                uid = uid,
                spawnTime = spawnTime,
            });
        }
    }
    public struct EntitySpawnData
    {
        public float spawnTime;
        public UID uid;
        public GameEntityPool factory;
    }
}
