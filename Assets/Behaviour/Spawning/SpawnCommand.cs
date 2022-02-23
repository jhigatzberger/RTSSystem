using JHiga.RTSEngine.CommandPattern;
using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "SpawnCommand", menuName = "RTS/Behaviour/Commands/SpawnCommand")]
    public class SpawnCommand : CommandProperties
    {
        [SerializeField] private ResourceData[] resourceEffect;
        [SerializeField] private float time;
        [SerializeField] private GameEntityPool spawn;
        [SerializeField] private CommandAction afterSpawnAction;
        public override string Description
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(spawn.name);
                foreach (ResourceData data in resourceEffect)
                {
                    stringBuilder.Append("\n");
                    stringBuilder.Append(RTSWorldData.Instance.resourceTypes[data.resourceType].name);
                    stringBuilder.Append(": ");
                    stringBuilder.Append(data.amount * -1);
                }
                return stringBuilder.ToString();
            }
        }
        public override bool IsApplicable(ICommandable entity, bool forced = false)
        {
            if (!LocalPlayerResources.Instance.CanAfford(resourceEffect))
                return false;
            if (forced && CommandInput.Instance.commandPreview != null)
            {
                if(CommandInput.Instance.commandPreview.TryGetComponent(out SpawnIndicator indicator))
                    return indicator.AllowSpawn;
            }
            return true;
        }

        public override Target PackTarget(ICommandable commandable)
        {
            if (requireContext)
                return Target.FromContext;
            else
                return new Target { position = commandable.Entity.GetExtension<ISpawner>().DefaultSpawnOffset };
        }

        public override void Request(ICommandable reference, IExtendableEntity[] selection, bool clearQueueOnEnqeue, Action<SkinnedCommand> callback)
        {
            ResourceEvents.RequestResourceAlter(
                new AlterResourceRequest(
                    reference.Entity.UID.player,
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
            return base.Build(reference, new IExtendableEntity[] { lowest.Entity },  clearQueueOnEnqeue);
        }

        public override void Execute(ICommandable commandable, ResolvedCommandReferences references)
        {
            commandable.Entity.GetExtension<ISpawner>().SpawnOffset = references.target.position;

            Action<IExtendableEntity> callback = null;
            if (afterSpawnAction != null)
                callback = (entity) => afterSpawnAction.Execute(commandable, InsertedEntity(entity, references));
            SpawnEvents.RequestSpawn(
                new SpawnRequest
                {
                    time = time,
                    poolIndex = spawn.Index,
                    spawnerUID = commandable.Entity.UID.unique,
                }, callback
            );            
        }
        private ResolvedCommandReferences InsertedEntity(IExtendableEntity entity, ResolvedCommandReferences references)
        {
            return new ResolvedCommandReferences
            {
                clearQueueOnEnqeue = references.clearQueueOnEnqeue,
                entities = references.entities,
                target = new Target
                {
                    position = references.target.position,
                    entity = entity
                }
            };
        }
    }
}
