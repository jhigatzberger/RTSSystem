using JHiga.RTSEngine.Spawning;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class SpawnNetwork : NetworkBehaviour
    {
        #region Singleton
        public static SpawnNetwork Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null || !IsServer || !IsOwner)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        #endregion

        [ServerRpc]
        public void RequestPlayerHomePositionEntityServerRpc()
        {
            SpawnPlayerHomeEntityClientRpc(SpawningServer.Instance.GetPlayerHomePosition());
        }
        [ClientRpc]
        public void SpawnPlayerHomeEntityClientRpc(Vector3 position)
        {
            SpawningClient.Instance.SpawnHomeBuilding(position, (int)OwnerClientId);
        }

        [ClientRpc]
        public void BroadCastEntityInitializationClientRpc(InitializationData data)
        {
            SpawningClient.Instance.SendAuthorizedData(data);
        }
    }
}