using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkLockStepManager : NetworkBehaviour
{
    [SerializeField] private float stepSize = 1f;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
            OnGameStart();
    }
    public void OnGameStart()
    {
        _LockStepServer server = gameObject.AddComponent<_LockStepServer>();
        server.manager = this;
        server.startTime = Time.time;
        server.stepSize = stepSize;
        server.OnStep = StepClientRPC;
    }

    [ClientRpc]
    private void StepClientRPC(float time, ulong count)
    {
        LockStep.Step(time, count);
    }

}

public class _LockStepServer : NetworkBehaviour
{
    internal NetworkLockStepManager manager;
    internal float startTime = 0;
    internal float stepSize = 1f;
    internal Action<float, ulong> OnStep;

    private ulong count;
    private float time;
    private void Update()
    {
        if (Time.time - startTime - time > stepSize)
        {
            time = Time.time - startTime;
            count++;
            OnStep(time, count);
        }
    }

}