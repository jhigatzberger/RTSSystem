using RTS.Entity.Selection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "MoveAction", menuName = "RTS/AI/Actions/MoveAction")]
    public class MoveAction : Action
    {
        public override void Enter(IStateMachine stateMachine)
        {
            BaseEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetComponent<IMovable>();
            movable.Move();
        }

        public override void Exit(IStateMachine stateMachine)
        {
            BaseEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetComponent<IMovable>();
            movable.Stop();
        }
    }
}
