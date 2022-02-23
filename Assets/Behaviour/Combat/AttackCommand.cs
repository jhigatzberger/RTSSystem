using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using UnityEngine;
using JHiga.RTSEngine.Selection;

[CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS/Behaviour/Commands/AttackCommand")]
public class AttackCommand : StateMachineCommandProperties
{
    public override bool IsApplicable(ICommandable entity, bool forced = false)
    {
        IExtendableEntity hovered = SelectionContext.FirstOrNullHovered;
        if(hovered != null && hovered.TryGetExtension(out IAttackable attackable)) // Can be optimized performance wise by using a tag for example
            if (PlayerContext.AreEnenmies(entity.Entity.UID.player, hovered.UID.player))
                return true;
        return false;
    }
}