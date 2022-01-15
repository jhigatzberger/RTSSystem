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
            int poolIndex = PlayerContext.players[playerId].faction.startEntityIndex;
            PlayerContext.players[playerId].Factories[poolIndex].Spawn(position, new UID(playerId, poolIndex, 0));
        }

        public void SendAuthorizedData(SpawnData data)
        {
            GameEntity.Get(new UID(data.spawnerUID)).GetExtension<ISpawner>().Enqueue(new UID(data.entityUID), data.time);
        }
    }
}