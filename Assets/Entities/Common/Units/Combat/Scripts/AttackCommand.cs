using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Team;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS/Behaviour/Commands/AttackCommand")]
public class AttackCommand : StateMachineCommandProperties
{
    public override bool Applicable(ICommandable entity)
    {
        GameEntity hovered = InteractableRegistry.FirstOrNullHovered;
        if(hovered != null && hovered.TryGetScriptableComponent(out IAttackable attackable)) // Can be optimized performance wise by using a tag for example
            if (PlayerContext.AreEnenmies(entity.Extendable.PlayerId, hovered.PlayerId))
                return true;
        return false;
    }
    public override CompiledCommand Compile()
    {
        return new CompiledCommand
        {
            commandID = CommandData.Instance.CommandToId[this],
            position = InteractableRegistry.FirstOrNullHovered.transform.position,
            targetID = InteractableRegistry.FirstOrNullHovered.EntityId
        };
    }
    protected override void OnExecute(ICommandable commandable, CompiledCommand data)
    {
        commandable.Extendable.GetScriptableComponent<IAttacker>().Target = InteractableRegistry.entities[data.targetID].GetScriptableComponent<IAttackable>();
        base.Execute(commandable, data);
    }
}