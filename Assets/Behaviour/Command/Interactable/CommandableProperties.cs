using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    [CreateAssetMenu(fileName = "DefaultCommandable", menuName = "RTS/Entity/Properties/Commandable")]
    public class CommandableProperties : ExtensionFactory
    {
        public CommandProperties[] commandCompetence;
        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new CommandableExtension(extendable, this);
        }
    }
}
