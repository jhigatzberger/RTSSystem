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
        public EntityFactory<GameEntity> spawn;
        public override bool Applicable(ICommandable entity)
        {
            return ResourceManager.playerResources >= cost;
        }

        public override Target PackTarget(ICommandable commandable)
        {
            Debug.Log(spawn.Index + " spawnindex");
            ResourceManager.Spend(cost);
            return commandable.Entity.GetScriptableComponent<ISpawner>().Waypoint;
        }

        public override void Execute(ICommandable commandable, Target target)
        {
            commandable.Entity.GetScriptableComponent<ISpawner>().Enqueue(spawn.Index, time);
            if (commandable.Entity.TryGetScriptableComponent(out IMovable movable))
                movable.Destination = target;
        }
    }
}
