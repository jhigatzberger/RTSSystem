using JHiga.RTSEngine.Spawning;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JHiga.RTSEngine.SinglePlayer
{
    public class SinglePlayerSpawning : MonoBehaviour
    {
        private void Start()
        {
            SpawnEvents.OnRequestSpawn += RequestEntityInitialization;
            SpawnStartEntities();
        }
        private void OnDestroy()
        {
            SpawnEvents.OnRequestSpawn -= RequestEntityInitialization;
        }
        public void SpawnStartEntities()
        {
            foreach (PlayerData player in PlayerContext.players)
            {
                List<StartEntityData> startEntities = PlayerContext.players[player.id].startEntities.OrderBy(s => s.offsetPosition.magnitude).ToList();
                for (int i = 0; i < startEntities.Count; i++)
                {
                    Debug.Log(player.id + " " + startEntities[i].entity.name +" " + startEntities[i].entity.Index);
                    UID uid = new UID(player.id, startEntities[i].entity.Index, i);
                    Debug.Log(uid.pool);
                    GameEntityPool.Get(uid).Spawn(startEntities[i].offsetPosition, uid);
                }
            }
        }
        public void RequestEntityInitialization(SpawnRequest spawnRequest)
        {
            int poolUID = UID.PoolShifted(spawnRequest.spawnerUID, spawnRequest.poolIndex);
            GameEntity.Get(new UID(spawnRequest.spawnerUID)).GetExtension<ISpawner>().Enqueue(new UID(UID.EntityShifted(poolUID, GameEntityPool.Get(poolUID).GenerateEntityID())), spawnRequest.time);
        }
    }
}