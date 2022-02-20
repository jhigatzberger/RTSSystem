using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.Selection;

namespace JHiga.RTSEngine.Construction
{
    public class BuildCommand : StateMachineCommandProperties
    {
        public override bool Applicable(ICommandable entity, bool forced = false)
        {
            IExtendableEntity hovered = SelectionContext.FirstOrNullHovered;
            if (hovered != null && hovered.TryGetExtension(out IBuildable buildable))
                return true;
            return false;
        }
    }

}
