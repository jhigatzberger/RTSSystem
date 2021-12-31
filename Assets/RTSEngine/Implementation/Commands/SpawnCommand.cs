using RTSEngine.Core.InputHandling;
using UnityEngine;

namespace RTSEngine.Core.Spawning
{
    [CreateAssetMenu(fileName = "SpawnCommand", menuName = "RTS/AI/Commands/SpawnCommand")]
    public class SpawnCommand : Command
    {
        public int cost;
        public float time;
        public RTSEntity spawn;
        public override bool Applicable(ICommandable entity)
        {
            return ResourceManager.playerResources >= cost;
        }

        public override CommandData Build(ICommandable entity)
        {
            ResourceManager.Spend(cost);
            return new CommandData
            {
                commandID = id
            };
        }

        public override void Execute(ICommandable commandable, CommandData data)
        {
            commandable.Behaviour.GetExtension<ISpawner>().Enqueue(spawn, time);
        }

    }

}
