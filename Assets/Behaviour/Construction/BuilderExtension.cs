using JHiga.RTSEngine.Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JHiga.RTSEngine.Construction
{
    public class BuilderExtension : BaseInteractableExtension<BuilderProperties>, IBuilder
    {
        public BuilderExtension(IExtendableEntity entity, BuilderProperties properties) : base(entity, properties)
        {
        }

        public int Speed => Properties.speed;

        public IBuildable Target => Entity.GetExtension<ITargeter>().Target.Value.entity.GetExtension<IBuildable>();

        public void Construct()
        {
            Target.CurrentConstructionLevel += Speed;
            if (Target.CurrentConstructionLevel >= Target.MaxConstructionLevel)
            {
                Entity.GetExtension<ISpawner>().SpawnOffset = Target.Entity.Position;
                SpawnEvents.RequestSpawn(new SpawnRequest
                {
                    spawnerUID = Entity.UniqueID.uniqueId,
                    poolIndex = Target.FinishiedEntity.Index,
                    time = 0
                });
                Entity.GetExtension<ITargeter>().Target = null;
            }
        }
    }
}