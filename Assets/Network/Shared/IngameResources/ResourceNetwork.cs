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
        public void AlterResourceServerRpc(int playerId, ResourceNetworkPayload request)
        {
            ResourceServer.Instance.AlterResource(playerId, request);
        }

        [ClientRpc]
        public void UpdateResourceClientRpc(ResourceNetworkPayload request, bool success, ClientRpcParams rpcParams = default)
        {
            Debug.Log("UpdateResourceClientRpc!");
            ResourceClient.Instance.UpdateResource(request, success);
        }
    }
}