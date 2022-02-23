using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "CanAttackDecision", menuName = "RTS/Behaviour/Decisions/CanAttackDecision")]
public class CanAttackDecision : StateMachineDecision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        return stateMachine.Entity.GetExtension<IAttacker>().CanAttack;
    }
}

