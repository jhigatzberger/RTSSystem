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
            Instance = this;
            SpawnEvents.OnRequestSpawn += RequestEntityInitialization;
            SpawnStartEntities();
        }
        private void OnDestroy()
        {
            SpawnEvents.OnRequestSpawn -= RequestEntityInitialization;
        }
        #endregion
        public Vector3 RandomPlayerHomePosition
        {
            get
            {
                return new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45));
            }
        }
        public void SpawnStartEntities()
        {
            foreach(PlayerProperties player in PlayerContext.players)
            {
                if(player.id == 0)
                    SpawnNetwork.Instance.SpawnStartEntitiesClientRpc(player.id);
                else
                    SpawnNetwork.Instance.SpawnStartEntitiesClientRpc(player.id, RandomPlayerHomePosition);
            }
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