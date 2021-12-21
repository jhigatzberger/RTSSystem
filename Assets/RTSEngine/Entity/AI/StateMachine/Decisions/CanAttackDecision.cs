using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.AI
{
    [CreateAssetMenu(fileName = "CanAttackDecision", menuName = "RTS/AI/Decisions/CanAttackDecision")]
    public class CanAttackDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            return stateMachine.Entity.GetComponent<IAttacker>().CanAttack;
        }
    }
}
