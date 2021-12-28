using RTSEngine.Entity.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.AI
{
    [CreateAssetMenu(fileName = "IsAttackingDecision", menuName = "RTS/AI/Decisions/IsAttackingDecision")]
    public class IsAttackingDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            return stateMachine.Entity.GetComponent<IAttacker>().IsAttacking;
        }
    }
}
