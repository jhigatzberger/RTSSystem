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
            Debug.Log("BroadCastEntityInitializationClientRpc");
            SpawningClient.Instance.SendAuthorizedData(data);
        }
    }
}