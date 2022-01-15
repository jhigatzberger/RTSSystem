using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public class SpawnerExtension : BaseInteractableExtension<SpawnerProperties>, ISpawner
    {
        private Queue<EntitySpawnData> spawnQueue = new Queue<EntitySpawnData>();
        private EntitySpawnData? next;
        private float timeStamp;
        private UID? nextUID = null;
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
            if (!nextUID.HasValue || !next.HasValue || next.Value.time > LockStep.time - timeStamp)
                return;
            
            next.Value.factory.Spawn(Entity.MonoBehaviour.transform.position + Properties.doorPosition, nextUID.Value);
            next = null;
            nextUID = null;
        }
        public void Enqueue(UID uid, float spawnTime)
        {
            spawnQueue.Enqueue(new EntitySpawnData
            {
                factory = GameEntityFactory.Get(uid),
                time = spawnTime
            });
        }
    }
    public struct EntitySpawnData
    {
        public float time;
        public GameEntityFactory factory;
    }
}
