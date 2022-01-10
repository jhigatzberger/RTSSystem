using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "IsAttackingDecision", menuName = "RTS/Behaviour/Decisions/IsAttackingDecision")]
public class IsAttackingDecision : Decision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        return stateMachine.Extendable.GetScriptableComponent<IAttacker>().IsAttacking;
    }
}

