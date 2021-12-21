using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.AI
{
    [CreateAssetMenu(fileName = "HasTargetDecision", menuName = "RTS/AI/Decisions/HasTargetDecision")]
    public class HasTargetDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            IAttacker attacker = stateMachine.Entity.GetComponent<IAttacker>();
            return attacker.Target != null && attacker.Target.IsAlive;
        }
    }

}
