using RTSEngine.Entity;
using RTSEngine.Entity.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
