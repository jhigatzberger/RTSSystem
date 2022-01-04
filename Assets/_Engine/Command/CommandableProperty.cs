using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    [CreateAssetMenu(fileName = "CommandableProperty", menuName = "RTS/Entity/Properties/CommandableProperty")]
    public class CommandableProperty : ExtensionFactory
    {
        public Command[] commandCompetence;
        public override IExtension Build(IExtendable extendable)
        {
            return new CommandableExtension(extendable, commandCompetence);
        }
    }
}
