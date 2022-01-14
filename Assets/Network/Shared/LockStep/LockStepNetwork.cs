using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LockStepNetwork : NetworkBehaviour
{
    #region Singleton
    public static LockStepNetwork Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    [ClientRpc]
    public void StepClientRPC(float time, ulong count)
    {
        LockStep.Step(time, count);
    }
}
