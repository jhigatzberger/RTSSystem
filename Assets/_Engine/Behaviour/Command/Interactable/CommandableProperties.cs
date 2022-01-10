using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    [CreateAssetMenu(fileName = "DefaultCommandable", menuName = "RTS/Entity/Properties/Commandable")]
    public class CommandableProperties : ExtensionFactory
    {
        public CommandProperties[] commandCompetence;
        public override IInteractableExtension Build(IExtendableInteractable extendable)
        {
            return new CommandableExtension(extendable, this);
        }
    }
}
