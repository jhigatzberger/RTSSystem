using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "StoppingDecision", menuName = "RTS/AI/Decisions/StoppingDecision")]
    public class StoppingDecision : Decision
    {
        public float stoppingDistance = 0.1f; 
        public override bool Decide(AIEntity entity)
        {
            NavMeshAgent agent = entity.gameObject.GetComponent<NavMeshAgent>();
            if (agent.remainingDistance <= stoppingDistance)
                return true;
            return false;
        }
    }

}
