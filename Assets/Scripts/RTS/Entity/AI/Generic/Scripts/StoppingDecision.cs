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
        public override bool Decide(IStateMachine stateMachine)
        {
            IMovable movable = stateMachine.Entity.GetComponent<IMovable>();            
            if (!movable.Destination.HasValue || Vector3.Distance(movable.Destination.Value, movable.Entity.transform.position) <= stoppingDistance)
                return true;
            return false;
        }
    }

}
