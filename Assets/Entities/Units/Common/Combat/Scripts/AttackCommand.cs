using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Team;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS/AI/Commands/AttackCommand")]
public class AttackCommand : CommandProperties
{
    public State state;
    public override bool Applicable(ICommandable entity)
    {
        GameEntity hovered = EntityManager.FirstOrNullHovered;
        if(hovered != null && hovered.TryGetScriptableComponent(out IAttackable attackable)) // Can be optimized performance wise by using a tag for example
            if (TeamContext.AreEnenmies(entity.Extendable.PlayerId, hovered.PlayerId))
                return true;
        return false;
    }

    public override CompiledCommand Compile()
    {
        return new CompiledCommand
        {
            commandID = id,
            position = EntityManager.FirstOrNullHovered.transform.position,
            targetID = EntityManager.FirstOrNullHovered.EntityId
        };
    }

    public override void Execute(ICommandable commandable, CompiledCommand data)
    {
        IExtendable entity = commandable.Extendable;
        entity.GetScriptableComponent<IAttacker>().Target = EntityManager.entities[data.targetID].GetScriptableComponent<IAttackable>();
        entity.GetScriptableComponent<IStateMachine>().ChangeState(state);
    }
}


