using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JHiga.RTSEngine.Spawning;
using System.Linq;

namespace JHiga.RTSEngine.Network
{
    public class SpawningServer : MonoBehaviour
    {
        #region Initialization
        public static SpawningServer Instance { get; private set; }
        private void Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            EntityConstants.OnRequireEntityID += RequestEntityInitialization;
        }
        #endregion
        public Vector3 GetPlayerHomePosition()
        {
            return new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45));
        }
        public void RequestEntityInitialization(int spawnID)
        {
            UID spawnUID = new UID(spawnID);
            int entityID = PlayerContext.players[spawnUID.playerIndex].Factories[spawnUID.poolIndex].GenerateEntityID();
            SpawnNetwork.Instance.BroadCastEntityInitializationClientRpc(
                    new InitializationData()
                    {
                        spawnID = spawnID,
                        entityID = entityID
                    }
                );
        }
    }
}