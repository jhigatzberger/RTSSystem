using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.Spawning;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "SpawnCommand", menuName = "RTS/AI/Commands/SpawnCommand")]
    public class SpawnCommand : Command
    {
        public int cost;
        public float time;
        public PooledEntityFactory spawn;
        public override bool Applicable(ICommandable entity)
        {
            return ResourceManager.playerResources >= cost;
        }

        public override CommandData Build()
        {
            ResourceManager.Spend(cost);
            return new CommandData
            {
                commandID = id
            };
        }

        public override void Execute(ICommandable commandable, CommandData data)
        {
            commandable.Extendable.GetScriptableComponent<ISpawner>().Enqueue(spawn, time);
        }

    }

}
