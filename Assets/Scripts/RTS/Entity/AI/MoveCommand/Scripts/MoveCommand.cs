using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "MoveCommand", menuName = "RTS/AI/Commands/MoveCommand")]
    public class MoveCommand : Command
    {
        public override bool Applicable(AIEntity entity)
        {
            return InputManager.worldPointerPosition.HasValue;
        }

        public override CommandData Build(AIEntity entity)
        {
            return new CommandData
            {
                commandID = id,
                position = InputManager.worldPointerPosition.Value,
                targetID = -1,
            };
        }
    }
}
