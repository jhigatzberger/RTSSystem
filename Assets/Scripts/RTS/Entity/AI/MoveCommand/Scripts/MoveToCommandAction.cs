using RTS.Entity.Selection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "MoveToCommandAction", menuName = "RTS/AI/Actions/MoveToCommandAction")]
    public class MoveToCommandAction : Action
    {
        public override void Enter(AIEntity entity)
        {
            entity.GetComponent<Animator>().SetFloat("Velocity", 1);
            NavMeshAgent agent = entity.GetComponent<NavMeshAgent>();
            CommandData data = entity.currentCommand.Value;            
            agent.SetDestination(Formation.Context.current.GetPosition(data.position.Value, entity.SelectionPosition, Context.entities.Count));
        }

        public override void Exit(AIEntity entity)
        {
            entity.GetComponent<Animator>().SetFloat("Velocity", 0);
            NavMeshAgent agent = entity.GetComponent<NavMeshAgent>();
            agent.SetDestination(entity.transform.position);
        }
    }
}
