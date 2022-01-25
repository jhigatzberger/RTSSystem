using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using JHiga.RTSEngine.Spawning;
using System.Linq;

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

        public void SpawnStartEntities(int playerId, Vector3 position = default)
        {
            List<StartEntityData> startEntities = PlayerContext.players[playerId].StartEntities.OrderBy(s => s.offsetPosition.magnitude).ToList();
            for (int i = 0; i < startEntities.Count; i++)
            {
                UID uid = new UID(playerId, startEntities[i].entity.Index, i);
                GameEntityPool.Get(uid).Spawn(position + startEntities[i].offsetPosition, uid);
            }
        }
        public void SendAuthorizedData(SpawnData data)
        {
            GameEntity.Get(new UID(data.spawnerUID)).GetExtension<ISpawner>().Enqueue(new UID(data.entityUID), data.time);
        }
    }
}
