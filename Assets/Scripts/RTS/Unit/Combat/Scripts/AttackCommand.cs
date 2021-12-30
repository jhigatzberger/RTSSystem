using RTSEngine.Entity;
using RTSEngine.Entity.AI;
using RTSEngine.Entity.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS/AI/Commands/AttackCommand")]
public class AttackCommand : Command
{
    public State state;
    public override bool Applicable(ICommandable entity)
    {
        BaseEntity hovered = EntityContext.FirstOrNullHovered;
        if(hovered != null && hovered.TryGetComponent(out IAttackable attackable)) // Can be optimized performance wise by using a tag for example
            if (RTSEngine.Team.Context.AreEnenmies(entity.Entity.Team, hovered.Team))
                return true;
        return false;
    }

    public override CommandData Build(ICommandable entity)
    {
        return new CommandData
        {
            commandID = id,
            position = EntityContext.FirstOrNullHovered.transform.position,
            targetID = EntityContext.FirstOrNullHovered.id
        };
    }

    public override void Execute(ICommandable commandable, CommandData data)
    {
        BaseEntity entity = commandable.Entity;
        entity.GetComponent<IAttacker>().Target = EntityContext.entities[data.targetID].GetComponent<IAttackable>();
        entity.GetComponent<IStateMachine>().ChangeState(state);
    }
}


