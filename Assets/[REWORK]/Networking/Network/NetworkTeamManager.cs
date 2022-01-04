using UnityEngine;
using Unity.Netcode;

namespace JHiga.RTSEngine.Team
{
    public class NetworkTeamManager : NetworkBehaviour
    {
        [SerializeField] PooledEntityFactory entity = null;
        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;
            TeamContext.PlayerTeam = (int)NetworkManager.Singleton.LocalClientId;

            entity.Spawn(new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45)), (int)NetworkManager.Singleton.LocalClientId, (int)NetworkManager.Singleton.LocalClientId);
        }
    }
}
