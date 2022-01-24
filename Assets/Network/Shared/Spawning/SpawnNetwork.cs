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

        [ClientRpc]
        public void SpawnStartEntitiesClientRpc(int playerID, Vector3 position = default)
        {
            SpawningClient.Instance.SpawnStartEntities(playerID, position);
        }
        [ClientRpc]
        public void BroadCastEntityInitializationClientRpc(SpawnData data)
        {
            SpawningClient.Instance.SendAuthorizedData(data);
        }
    }
}