using Unity.Netcode;
using UnityEngine;

public class NetworkLockStepManager : NetworkBehaviour
{
    [SerializeField] private float stepSize = 1f;

    NetworkVariable<float> CurrentStepStamp = new NetworkVariable<float>();

    private void OnEnable()
    {
        CurrentStepStamp.OnValueChanged += Step;
    }

    private void Update()
    {
        if (IsServer)
            ServerCheck();   
    }
    private void Step(float oldTick, float newTick)
    {
        LockStep.Step(newTick);
    }
    private void ServerCheck()
    {
        if (Time.time - CurrentStepStamp.Value > stepSize)
            CurrentStepStamp.Value = Time.time;
    }

}
