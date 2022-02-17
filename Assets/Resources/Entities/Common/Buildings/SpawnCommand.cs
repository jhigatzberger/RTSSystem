using JHiga.RTSEngine.CommandPattern;
using System;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "SpawnCommand", menuName = "RTS/Behaviour/Commands/SpawnCommand")]
    public class SpawnCommand : CommandProperties
    {
        public ResourceData resourceEffect;
        public float time;
        public GameEntityPool spawn;
        public override bool Applicable(ICommandable entity)
        {
            return LocalPlayerResources.Instance.CanAfford(resourceEffect);
        }

        public override Target PackTarget(ICommandable commandable)
        {
            return commandable.Entity.GetExtension<ISpawner>().Waypoint;
        }

        public override void Request(ICommandable reference, IExtendableEntity[] selection, bool clearQueueOnEnqeue, Action<SkinnedCommand> callback)
        {
            ResourceEvents.RequestResourceAlter(
                new AlterResourceRequest(
                    resourceEffect,
                    () => base.Request(reference, selection, clearQueueOnEnqeue, callback)
                )
           );
        }
                
        public override ResolvedCommand Build(ICommandable reference, IExtendableEntity[] selection, bool clearQueueOnEnqeue)
        {
            ISpawner lowest = null;
            foreach(IExtendableEntity entity in selection)
            {
                if (entity.TryGetExtension(out ISpawner spawner) && lowest == null || lowest.QueueSize > spawner.QueueSize)
                    lowest = spawner;
            }
            return base.Build(reference, new IExtendableEntity[] { lowest.Entity },clearQueueOnEnqeue);
        }

        public override void Execute(ICommandable commandable, ResolvedCommandReferences references)
        {
            SpawnEvents.RequestSpawn(
                new SpawnRequest {
                    time = time,
                    poolIndex = spawn.Index,
                    spawnerUID = commandable.Entity.UniqueID.uniqueId
                }
            );            
        }
    }
}
