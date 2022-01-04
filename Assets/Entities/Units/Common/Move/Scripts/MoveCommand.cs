using JHiga.RTSEngine.AI.Formation;
using JHiga.RTSEngine.InputHandling;
using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveCommand", menuName = "RTS/AI/Commands/MoveCommand")]
public class MoveCommand : Command
{
    public State state;
    public override bool Applicable(ICommandable entity)
    {
        return InputManager.worldPointerPosition.HasValue;
    }
    public override void Execute(ICommandable commandable, CommandData command)
    {
        IExtendable entity = commandable.Extendable;
        entity.GetScriptableComponent<IStateMachine>().ChangeState(state);
        entity.GetScriptableComponent<IMovable>().Enqueue(FormationContext.current.GetPosition(command.position, commandable.Extendable));
    }
    public override CommandData Build()
    {
        return new CommandData
        {
            commandID = id,
            position = InputManager.worldPointerPosition.Value,
        };
    }
}

