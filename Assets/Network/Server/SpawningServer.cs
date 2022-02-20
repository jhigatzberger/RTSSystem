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
        public static SpawningServer Instance { get; private set; }
        private void Start()
        {
            Instance = this;
            SpawnEvents.OnRequestSpawn += RequestEntityInitialization;
            SpawnStartEntities();
        }
        private void OnDestroy()
        {
            SpawnEvents.OnRequestSpawn -= RequestEntityInitialization;
        }
        public void SpawnStartEntities()
        {
            foreach(PlayerData player in PlayerContext.players)
                SpawnNetwork.Instance.SpawnStartEntitiesClientRpc(player.id);
        }
        public void RequestEntityInitialization(SpawnRequest spawnRequest)
        {
            Debug.Log("RequestEntityInitialization");
            int poolUID = UID.PoolShifted(spawnRequest.spawnerUID, spawnRequest.poolIndex);
            SpawnNetwork.Instance.BroadCastEntityInitializationClientRpc(
                new SpawnData()
                {
                    spawnerUID = spawnRequest.spawnerUID,
                    entityUID = UID.EntityShifted(poolUID, GameEntityPool.Get(poolUID).GenerateEntityID()),
                    time = spawnRequest.time
                }
            );
        }
    }
}