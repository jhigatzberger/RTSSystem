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

        public SpawnerExtension(IExtendable entity, SpawnerProperties properties) : base(entity, properties)
        {
            LockStep.OnStep += LockStep_OnStep;
        }

        public void AuthorizeID(int uniqueId)
        {
            nextUID = new UID(uniqueId);
        }

        public void RequestNext()
        {
            if (spawnQueue.Count == 0 || next != null || nextUID.HasValue)
                return;
            next = spawnQueue.Dequeue();
            EntityConstants.RequireEntityID(Entity.EntityId.uniqueId);
            timeStamp = LockStep.time;
        }

        private void DoSpawn()
        {
            if (!nextUID.HasValue || !next.HasValue || next.Value.time > LockStep.time - timeStamp)
                return;

            next.Value.toSpawn.Spawn(Entity.MonoBehaviour.transform.position + Properties.doorPosition, nextUID.Value.uniqueId);
            next = null;
            nextUID = null;
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
        public void Enqueue(int entityType, float spawnTime)
        {
            spawnQueue.Enqueue(new EntitySpawnData
            {
                toSpawn = PlayerContext.players[Entity.EntityId.playerIndex].Factories[entityType],
                time = spawnTime
            });
        }
    }

    public struct EntitySpawnData
    {
        public float time;
        public EntityFactory<GameEntity> toSpawn;
    }
}
