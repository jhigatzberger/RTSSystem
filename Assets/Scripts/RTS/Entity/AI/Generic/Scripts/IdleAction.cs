using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "IdleAction", menuName = "RTS/AI/Actions/IdleAction")]
    public class IdleAction : Action
    {
        public override void Enter(AIEntity entity)
        {            
        }

        public override void Exit(AIEntity entity)
        {
        }
    }
}
