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

        private Dictionary<int, int[]> playerResources = new Dictionary<int, int[]>();
        private void Start()
        {
            foreach(PlayerData player in PlayerContext.players)
                playerResources.Add(player.id, RTSWorldData.Instance.resourceTypes.Select(r => r.startAmount).ToArray());            
        }

        public void AlterResource(int playerId, ResourceNetworkPayload request)
        {
            Debug.Log("AlterResource!");
            bool success = true;

            foreach(ResourceData data in request.data)
            {
                if (playerResources[playerId][data.resourceType] + data.amount < 0 && data.amount < 0)
                    success = false;
            }
            if(success)
            {
                foreach (ResourceData data in request.data)
                    playerResources[playerId][data.resourceType] += data.amount;
            }

            ResourceNetwork.Instance.UpdateResourceClientRpc(
                new ResourceNetworkPayload
                {
                    data = request.data.Select(d=> new ResourceData { resourceType = d.resourceType, amount = playerResources[playerId][d.resourceType] }).ToArray(),
                    successCallback = request.successCallback
                }, success, new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new ulong[] { NetworkGameManager.PlayerToClient(playerId) }
                    }
            });
        }
    }
}