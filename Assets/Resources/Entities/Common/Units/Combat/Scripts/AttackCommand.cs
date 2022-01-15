using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using UnityEngine;
using JHiga.RTSEngine.Selection;

[CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS/Behaviour/Commands/AttackCommand")]
public class AttackCommand : StateMachineCommandProperties
{
    public override bool Applicable(ICommandable entity)
    {
        IExtendableEntity hovered = SelectionContext.FirstOrNullHovered;
        if(hovered != null && hovered.TryGetExtension(out IAttackable attackable)) // Can be optimized performance wise by using a tag for example
            if (PlayerContext.AreEnenmies(entity.Entity.UniqueID.playerIndex, hovered.UniqueID.playerIndex))
                return true;
        return false;
    }
    public override Target PackTarget(ICommandable commandable)
    {
        return new Target
        {
            position = SelectionContext.FirstOrNullHovered.MonoBehaviour.transform.position,
            entity = SelectionContext.FirstOrNullHovered
        };
    }
    protected override void BeforeStateChange(ICommandable commandable, Target target)
    {
        commandable.Entity.GetExtension<IAttacker>().Target = target.entity.GetExtension<IAttackable>();
    }

}