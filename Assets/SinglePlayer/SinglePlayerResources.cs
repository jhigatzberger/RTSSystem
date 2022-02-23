using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JHiga.RTSEngine.SinglePlayer
{
    public class SinglePlayerResources : MonoBehaviour
    {
        private Dictionary<int, int[]> playerResources = new Dictionary<int, int[]>();
        private void Start()
        {
            ResourceEvents.OnAlterResourceRequest += ResourceEvents_OnAlterResourceRequest;
            foreach (PlayerData player in PlayerContext.players)
                playerResources.Add(player.id, RTSWorldData.Instance.resourceTypes.Select(r => r.startAmount).ToArray());
        }
        private void OnDestroy()
        {
            ResourceEvents.OnAlterResourceRequest -= ResourceEvents_OnAlterResourceRequest;
        }

        private void ResourceEvents_OnAlterResourceRequest(AlterResourceRequest request)
        {
            bool success = true;
            foreach (ResourceData data in request.data)
            {
                if (playerResources[request.playerId][data.resourceType] + data.amount < 0 && data.amount < 0)
                    success = false;
            }
            if (success)
            {
                foreach (ResourceData data in request.data)
                    playerResources[request.playerId][data.resourceType] += data.amount;
            }
            if (success)
                request.successCallback();
            foreach (ResourceData data in request.data)
                ResourceEvents.UpdateResource(new ResourceData { resourceType = data.resourceType, amount = playerResources[request.playerId][data.resourceType] });
        }
    }
}