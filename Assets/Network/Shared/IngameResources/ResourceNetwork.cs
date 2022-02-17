using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class ResourceNetwork : NetworkBehaviour
    {
        public static ResourceNetwork Instance;
        private void Awake()
        {
            Instance = this;
        }

        [ServerRpc(RequireOwnership = false)]
        public void AlterResourceServerRpc(ulong clientId, int resourceType, int amount, int callbackId)
        {
            ResourceServer.Instance.AlterResource(clientId, resourceType, amount, callbackId);
        }

        [ClientRpc]
        public void UpdateResourceClientRpc(int resourceType, int amount, int callbackId, bool success, ClientRpcParams rpcParams = default)
        {
            Debug.Log("UpdateResourceClientRpc!");
            ResourceClient.Instance.UpdateResource(new ResourceData
            {
                resourceType = resourceType,
                amount = amount
            }, callbackId, success);
        }
    }
}