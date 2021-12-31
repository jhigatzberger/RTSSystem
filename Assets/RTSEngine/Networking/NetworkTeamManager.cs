using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using RTSEngine.Core;

namespace RTSEngine.Team
{
    public class NetworkTeamManager : NetworkBehaviour
    {
        [SerializeField] GameObject prefab = null;
        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;
            Context.PlayerTeam = (int)NetworkManager.Singleton.LocalClientId;

            RTSBehaviour behaviour = Instantiate(prefab, new Vector3(Random.Range(-45,45),0,Random.Range(-45,45)), Quaternion.identity).GetComponent<RTSBehaviour>();
            behaviour.id = (int)NetworkManager.Singleton.LocalClientId;
            behaviour.Team = (int)NetworkManager.Singleton.LocalClientId;
        }
    }
}
