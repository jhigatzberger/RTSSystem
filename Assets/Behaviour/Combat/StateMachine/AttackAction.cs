using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "RTS/Behaviour/Actions/AttackAction")]
public class AttackAction : StateMachineAction
{
    public override void Enter(IStateMachine stateMachine)
    {
        stateMachine.Entity.GetExtension<IAttacker>().Attack();
    }

    public override void Exit(IStateMachine entity)
    {
    }
        
}
