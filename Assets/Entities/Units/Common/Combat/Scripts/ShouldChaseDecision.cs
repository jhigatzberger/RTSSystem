using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Combat;
using UnityEngine;


[CreateAssetMenu(fileName = "ShouldChaseDecision", menuName = "RTS/AI/Decisions/ShouldChaseDecision")]
public class ShouldChaseDecision : Decision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        IAttacker attacker = stateMachine.Extendable.GetScriptableComponent<IAttacker>();
        return attacker.Target != null && attacker.Target.IsAlive && !attacker.IsInRange;
    }
}

