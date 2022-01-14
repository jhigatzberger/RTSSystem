using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JHiga.RTSEngine.Spawning;

namespace JHiga.RTSEngine.Network
{
    public class SpawningClient : MonoBehaviour
    {
        #region Singleton
        public static SpawningClient Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        #endregion

        private void Start()
        {
            SpawnNetwork.Instance.RequestPlayerHomePositionEntityServerRpc();
        }

        public void SpawnHomeBuilding(Vector3 position, int playerId)
        {
            PlayerContext.players[playerId].Factories[PlayerContext.players[playerId].faction.startEntityIndex].Spawn(position, playerId);
        }

        public void SendAuthorizedData(InitializationData data)
        {
            EntityConstants.FindEntityByUniqueId(new UID(data.spawnID)).GetScriptableComponent<ISpawner>().AuthorizeID(data.entityID);
        }
    }
}