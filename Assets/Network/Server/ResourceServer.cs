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

        public void AlterResource(ulong clientId, int resourceType, int amount, int callbackId)
        {
            Debug.Log("AlterResource!");
            bool success = true;
            if (playerResources[clientId][resourceType] + amount < 0 && amount<0)
                success = false;
            else
                playerResources[clientId][resourceType] += amount;

            ResourceNetwork.Instance.UpdateResourceClientRpc(resourceType, playerResources[clientId][resourceType], callbackId, success, new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { clientId }
                }
            });
        }
    }
}