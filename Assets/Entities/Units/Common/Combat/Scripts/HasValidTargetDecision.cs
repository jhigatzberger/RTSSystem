using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Combat;
using UnityEngine;


[CreateAssetMenu(fileName = "HasValidTargetDecision", menuName = "RTS/AI/Decisions/HasValidTargetDecision")]
public class HasValidTargetDecision : Decision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        IAttacker attacker = stateMachine.Extendable.GetScriptableComponent<IAttacker>();
        return attacker.Target != null && attacker.Target.IsAlive;
    }
}

