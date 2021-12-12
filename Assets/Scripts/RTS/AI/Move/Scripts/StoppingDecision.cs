using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.AI
{
    [CreateAssetMenu(fileName = "StoppingDecision", menuName = "RTS/AI/StoppingDecision")]
    public class StoppingDecision : Decision
    {
        public float stoppingDistance = 0.1f; 
        public override bool Decide(AIEntity entity)
        {
            if (Vector3.Distance(entity.currentCommand.Value.position.Value, entity.transform.position) <= stoppingDistance)
            {
                Debug.Log("TRUE");
                return true;
            }
            Debug.Log("FALSE");
            return false;
        }
    }

}
