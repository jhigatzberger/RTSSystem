using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using UnityEngine;
using JHiga.RTSEngine.Selection;

[CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS/Behaviour/Commands/AttackCommand")]
public class AttackCommand : StateMachineCommandProperties
{
    public override bool ApplicableFromContext(ICommandable entity, bool forced = false)
    {
        IExtendableEntity hovered = SelectionContext.FirstOrNullHovered;
        if(hovered != null && hovered.TryGetExtension(out IAttackable attackable)) // Can be optimized performance wise by using a tag for example
            if (PlayerContext.AreEnenmies(entity.Entity.UniqueID.playerIndex, hovered.UniqueID.playerIndex))
                return true;
        return false;
    }
}