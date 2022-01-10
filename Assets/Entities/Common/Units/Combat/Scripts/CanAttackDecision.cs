using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "CanAttackDecision", menuName = "RTS/Behaviour/Decisions/CanAttackDecision")]
public class CanAttackDecision : Decision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        return stateMachine.Extendable.GetScriptableComponent<IAttacker>().CanAttack;
    }
}

