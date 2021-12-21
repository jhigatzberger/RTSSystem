using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "AttackAction", menuName = "RTS/AI/Actions/AttackAction")]
    public class AttackAction : Action
    {
        public override void Enter(IStateMachine entity)
        {
            entity.Entity.GetComponent<IAttacker>().Attack();
        }

        public override void Exit(IStateMachine entity)
        {
        }
        
    }
}