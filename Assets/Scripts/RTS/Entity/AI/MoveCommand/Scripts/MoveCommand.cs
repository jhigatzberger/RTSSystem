using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
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
            BaseEntity entity = commandable.Entity;
            entity.GetComponent<IStateMachine>().ChangeState(state);
            entity.GetComponent<IMovable>().Enqueue(command.position);
        }
        public override CommandData Build(ICommandable entity)
        {
            return new CommandData
            {
                commandID = id,
                position = Formation.Context.current.GetPosition(InputManager.worldPointerPosition.Value, entity.Entity),
                targetID = -1,
            };
        }
    }
}
