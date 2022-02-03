using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class ResourceManager : NetworkBehaviour
{
    private static List<int> resources;

    public static int playerResources;
    public static event Action<int> OnResourceUpdate;

    private static ResourceManager instance;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        instance = this;
        if (IsServer)
        {
            resources = new List<int>();
            foreach (ulong clientID in NetworkManager.ConnectedClientsIds)
                resources.Add(0);
            LockStep.OnStep += LockStep_OnStep;
        }
    }


    public static void Spend(int amount, Action successCallback)
    {
        instance.SpendResourceServerRPC(NetworkManager.Singleton.LocalClientId, -amount);
        successCallback();
    }

    [ClientRpc]
    private void UpdateResourceClientRPC(int amount, ClientRpcParams clientRpcParams = default)
    {
        playerResources = amount;
        OnResourceUpdate?.Invoke(amount);
    }

    [ServerRpc]
    private void SpendResourceServerRPC(ulong clientID, int amount)
    {
        if (amount > 0)
            return;
        SetResource(clientID, resources[(int)clientID] + amount);
    }

    public float resourcesPerSecond;
    private float resourceScraps;
    private float lastTime;

    private void LockStep_OnStep()
    {
        resourceScraps += (LockStep.time - lastTime) * resourcesPerSecond;
        if (resourceScraps > 1)
        {
            int amount = (int)resourceScraps;
            resourceScraps -= amount;
            for (int i = 0; i < resources.Count; i++)
                SetResource((ulong)i, resources[i] + amount);
        }
        lastTime = LockStep.time;
    }


    private void SetResource(ulong clientID, int amount)
    {
        resources[(int)clientID] = amount;
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientID }
            }
        };
        UpdateResourceClientRPC(amount, clientRpcParams);
    }

}
