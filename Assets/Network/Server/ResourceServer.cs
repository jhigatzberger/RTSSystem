using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

namespace JHiga.RTSEngine.Network
{
    public class ResourceServer : MonoBehaviour
    {
        public static ResourceServer Instance;
        private void Awake()
        {
            Instance = this;
        }

        private Dictionary<ulong, int[]> playerResources = new Dictionary<ulong, int[]>();
        private void Start()
        {
            foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
                playerResources.Add(clientId,RTSWorldData.Instance.resourceTypes.Select(r => r.startAmount).ToArray());            
        }

        public void AlterResource(ulong clientId, ResourceNetworkPayload request)
        {
            Debug.Log("AlterResource!");
            bool success = true;

            foreach(ResourceData data in request.data)
            {
                if (playerResources[clientId][data.resourceType] + data.amount < 0 && data.amount < 0)
                    success = false;
            }
            if(success)
            {
                foreach (ResourceData data in request.data)
                    playerResources[clientId][data.resourceType] += data.amount;
            }

            ResourceNetwork.Instance.UpdateResourceClientRpc(
                new ResourceNetworkPayload
                {
                    data = request.data.Select(d=> new ResourceData { resourceType = d.resourceType, amount = playerResources[clientId][d.resourceType] }).ToArray(),
                    successCallback = request.successCallback
                }, success, new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { clientId }
                    }
            });
        }
    }
}