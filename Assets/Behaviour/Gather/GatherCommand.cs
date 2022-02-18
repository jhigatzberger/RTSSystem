using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.Selection;

namespace JHiga.RTSEngine.Gather
{
    public class GatherCommand : StateMachineCommandProperties
    {
        public override bool Applicable(ICommandable entity, bool forced = false)
        {
            IExtendableEntity hovered = SelectionContext.FirstOrNullHovered;
            if (hovered != null && hovered.TryGetExtension(out IGatherable gatherable)) // Can be optimized performance wise by using a tag for example
                return true;
            return false;
        }
    }
}
