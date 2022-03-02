using JHiga.RTSEngine;
using JHiga.RTSEngine.Spawning;
using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnAction", menuName = "RTS/Behaviour/Actions/SpawnAction")]
public class SpawnAction : StateMachineAction
{
    [SerializeField] private GameEntityPool entity;
    [SerializeField] private float delay;
    public override void Enter(IStateMachine stateMachine)
    {
        SpawnEvents.RequestSpawn(new SpawnRequest
        {
            spawnerUID = stateMachine.Entity.UID.unique,
            poolIndex = entity.Index,
            time = delay
        });
    }

    public override void Exit(IStateMachine stateMachine)
    {
    }

}
