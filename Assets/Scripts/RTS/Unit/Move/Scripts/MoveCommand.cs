using RTSEngine.Core;
using RTSEngine.Core.AI;
using RTSEngine.Core.AI.Formation;
using RTSEngine.Core.InputHandling;
using RTSEngine.Core.Movement;
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
        RTSBehaviour entity = commandable.Behaviour;
        entity.GetComponent<IStateMachine>().ChangeState(state);
        entity.GetComponent<IMovable>().Enqueue(command.position);
    }
    public override CommandData Build(ICommandable entity)
    {
        return new CommandData
        {
            commandID = id,
            position = FormationContext.current.GetPosition(InputManager.worldPointerPosition.Value, entity.Behaviour),
        };
    }
}

