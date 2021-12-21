using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "ChaseAction", menuName = "RTS/AI/Actions/ChaseAction")]
    public class ChaseAction : Action
    {
        public override void Enter(IStateMachine stateMachine)
        {
            BaseEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetComponent<IMovable>();
            movable.Clear();
            IAttacker attacker = entity.GetComponent<IAttacker>();
            movable.Enqueue(attacker.Target.Entity);
        }

        public override void Exit(IStateMachine stateMachine)
        {
            BaseEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetComponent<IMovable>();
            movable.Stop();
        }
    }
}