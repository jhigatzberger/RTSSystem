using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "IsAttackingDecision", menuName = "RTS/Behaviour/Decisions/IsAttackingDecision")]
public class IsAttackingDecision : StateMachineDecision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        return stateMachine.Entity.GetExtension<IAttacker>().IsAttacking;
    }
}

