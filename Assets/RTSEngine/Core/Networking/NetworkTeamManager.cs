using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace RTSEngine.Team
{
    public class NetworkTeamManager : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;
            Context.playerTeam = (int)NetworkManager.Singleton.LocalClientId;
        }
    }
}
