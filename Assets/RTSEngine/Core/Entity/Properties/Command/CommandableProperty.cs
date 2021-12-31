using UnityEngine;

namespace RTSEngine.Core.InputHandling
{
    [CreateAssetMenu(fileName = "CommandableProperty", menuName = "RTS/Entity/Properties/CommandableProperty")]
    public class CommandableProperty : RTSProperty
    {
        public Command[] commandCompetence;
        public override IExtension Build(RTSBehaviour behaviour)
        {
            return new CommandableExtension(behaviour, commandCompetence);
        }
    }
}
