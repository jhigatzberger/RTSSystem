using Unity.Netcode;
using UnityEngine;

public class NetworkLockStepManager : NetworkBehaviour
{
    [SerializeField] private float stepSize = 1f;

    NetworkVariable<float> CurrentStepStamp = new NetworkVariable<float>();
    float lastStepStamp = 0;

    private void Update()
    {
        if (NetworkManager.Singleton.IsServer)
            ServerCheck();

        if (lastStepStamp != CurrentStepStamp.Value)
            Step();
    }
    private void Step()
    {
        lastStepStamp = CurrentStepStamp.Value;
        LockStep.Step(lastStepStamp);
    }
    private void ServerCheck()
    {
        if (Time.time - lastStepStamp > stepSize)
            CurrentStepStamp.Value = Time.time;
    }

}
