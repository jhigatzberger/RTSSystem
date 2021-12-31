using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using RTSEngine.Core;

namespace RTSEngine.Team
{
    public class NetworkTeamManager : NetworkBehaviour
    {
        [SerializeField] RTSEntity entity = null;
        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;
            TeamContext.PlayerTeam = (int)NetworkManager.Singleton.LocalClientId;

            entity.Instantiate(new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45)), (int)NetworkManager.Singleton.LocalClientId, (int)NetworkManager.Singleton.LocalClientId);
        }
    }
}
