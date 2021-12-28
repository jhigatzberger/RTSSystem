using RTSEngine.Entity.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.AI
{
    [CreateAssetMenu(fileName = "ShouldChaseDecision", menuName = "RTS/AI/Decisions/ShouldChaseDecision")]
    public class ShouldChaseDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            IAttacker attacker = stateMachine.Entity.GetComponent<IAttacker>();
            return attacker.Target != null && attacker.Target.IsAlive && !attacker.IsInRange;
        }
    }
}
