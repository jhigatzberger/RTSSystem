using JHiga.RTSEngine.AI.Formation;
using JHiga.RTSEngine.InputHandling;
using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.CommandPattern;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveCommand", menuName = "RTS/Behaviour/Commands/MoveCommand")]
public class MoveCommand : StateMachineCommandProperties
{
    public override bool Applicable(ICommandable entity)
    {
        return InputManager.worldPointerPosition.HasValue;
    }
    public override CompiledCommand Compile()
    {
        return new CompiledCommand
        {
            commandID = CommandData.Instance.CommandToId[this],
            position = InputManager.worldPointerPosition.Value,
        };
    }
    protected override void OnExecute(ICommandable commandable, CompiledCommand data)
    {
        commandable.Extendable.GetScriptableComponent<IMovable>().Enqueue(FormationContext.current.GetPosition(data.position, commandable.Extendable));
    }
}

