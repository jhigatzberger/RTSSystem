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

        public void SpawnStartEntities(Vector3 position, int playerId)
        {
            StartEntityData[] startEntities = PlayerContext.players[playerId].faction.startEntities;
            for (int i = 0; i< startEntities.Length; i++)
            {
                UID uid = new UID(playerId, startEntities[i].entity.Index, i);
                GameEntityPool.Get(uid).Spawn(position + startEntities[i].offsetPosition, uid);
            }
        }

        public void SendAuthorizedData(SpawnData data)
        {
            Debug.Log("SendAuthorizedData!");
            GameEntity.Get(new UID(data.spawnerUID)).GetExtension<ISpawner>().Enqueue(new UID(data.entityUID), data.time);
        }
    }
}