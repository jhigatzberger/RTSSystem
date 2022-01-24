using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.Movement;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "SpawnCommand", menuName = "RTS/Behaviour/Commands/SpawnCommand")]
    public class SpawnCommand : CommandProperties
    {
        public int cost;
        public float time;
        public GameEntityPool spawn;
        public override bool Applicable(ICommandable entity)
        {
            return ResourceManager.playerResources >= cost;
        }

        public override Target PackTarget(ICommandable commandable)
        {
            return commandable.Entity.GetExtension<ISpawner>().Waypoint;
        }

        public override void Execute(ICommandable commandable, Target target)
        {
            Debug.Log("exectuting!");
            if(ResourceManager.playerResources >= cost)
            {
                ResourceManager.Spend(cost, ()=>SpawnEvents.RequestSpawn(new SpawnRequest { time = time, poolIndex = spawn.Index, spawnerUID = commandable.Entity.UniqueID.uniqueId }));
            }
        }
    }
}
