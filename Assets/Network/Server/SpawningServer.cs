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
            SpawnEvents.OnRequestSpawn += RequestEntityInitialization;
        }
        #endregion
        public Vector3 GetPlayerHomePosition()
        {
            return new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45));
        }
        public void RequestEntityInitialization(SpawnRequest spawnRequest)
        {
            SpawnNetwork.Instance.BroadCastEntityInitializationClientRpc(
                    new SpawnData()
                    {
                        spawnerUID = spawnRequest.spawnerUID,
                        entityUID = GameEntityFactory.Get(new UID(spawnRequest.spawnerUID)).GenerateEntityID(),
                        time = spawnRequest.time
                    }
                );
        }
    }
}