using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.Spawning;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "SpawnCommand", menuName = "RTS/Behaviour/Commands/SpawnCommand")]
    public class SpawnCommand : CommandProperties
    {
        public int cost;
        public float time;
        public PooledGameEntityFactory spawn;
        public override bool Applicable(ICommandable entity)
        {
            return ResourceManager.playerResources >= cost;
        }

        public override CompiledCommand Compile()
        {
            ResourceManager.Spend(cost);
            return new CompiledCommand
            {
                commandID = CommandData.Instance.CommandToId[this]
            };
        }

        public override void Execute(ICommandable commandable, CompiledCommand data)
        {
            commandable.Extendable.GetScriptableComponent<ISpawner>().Enqueue(spawn, time);
            Debug.Log("egg");
        }

    }

}
