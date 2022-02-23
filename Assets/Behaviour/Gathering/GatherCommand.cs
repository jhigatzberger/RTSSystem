using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.Selection;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public class GatherCommand : StateMachineCommandProperties
    {
        public int resourceType;
        public override bool IsApplicable(ICommandable entity, bool forced = false)
        {
            IExtendableEntity hovered = SelectionContext.FirstOrNullHovered;
            if (hovered != null && hovered.TryGetExtension(out IGatherable gatherable) && gatherable.ResourceType == resourceType)
            {
                Debug.Log("applicable " + name);
                return true;
            }
            return false;
        }
    }
}
