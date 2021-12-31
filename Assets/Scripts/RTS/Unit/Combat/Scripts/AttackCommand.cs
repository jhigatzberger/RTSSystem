using RTSEngine.Core;
using RTSEngine.Core.AI;
using RTSEngine.Core.Combat;
using RTSEngine.Core.InputHandling;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS/AI/Commands/AttackCommand")]
public class AttackCommand : Command
{
    public State state;
    public override bool Applicable(ICommandable entity)
    {
        RTSBehaviour hovered = EntityContext.FirstOrNullHovered;
        if(hovered != null && hovered.TryGetExtension(out IAttackable attackable)) // Can be optimized performance wise by using a tag for example
            if (RTSEngine.Team.TeamContext.AreEnenmies(entity.Behaviour.Team, hovered.Team))
                return true;
        return false;
    }

    public override CommandData Build(ICommandable entity)
    {
        return new CommandData
        {
            commandID = id,
            position = EntityContext.FirstOrNullHovered.transform.position,
            targetID = EntityContext.FirstOrNullHovered.Id
        };
    }

    public override void Execute(ICommandable commandable, CommandData data)
    {
        RTSBehaviour entity = commandable.Behaviour;
        entity.GetExtension<IAttacker>().Target = EntityContext.entities[data.targetID].GetExtension<IAttackable>();
        entity.GetExtension<IStateMachine>().ChangeState(state);
    }
}


