using RTSEngine.Entity.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "WanderAction", menuName = "RTS/AI/Actions/WanderAction")]
public class WanderAction : Action
{
    public float maxDistance;
    public override void Enter(IStateMachine stateMachine)
    {
        /*
        entity.GetComponent<Animator>().SetFloat("Velocity", 1);
        NavMeshAgent agent = entity.GetComponent<NavMeshAgent>();

        Vector3 randomPos = Random.insideUnitSphere * maxDistance + entity.transform.position; // deterministic random
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        agent.SetDestination(hit.position);*/
    }

    public override void Exit(IStateMachine stateMachine)
    {
        /*entity.GetComponent<Animator>().SetFloat("Velocity", 0);
        NavMeshAgent agent = entity.GetComponent<NavMeshAgent>();
        agent.SetDestination(entity.transform.position);*/
    }
    
}
