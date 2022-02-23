using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Combat;
using UnityEngine;


[CreateAssetMenu(fileName = "ShouldChaseDecision", menuName = "RTS/Behaviour/Decisions/ShouldChaseDecision")]
public class ShouldChaseDecision : StateMachineDecision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        IAttacker attacker = stateMachine.Entity.GetExtension<IAttacker>();
        return attacker.Target != null && attacker.Target.Value.HasActiveEntity && !attacker.IsInRange;
    }
}

