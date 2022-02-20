using System;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public class StartEntityMarker : MonoBehaviour
    {
        [SerializeField] private StartSpawnData[] startSpawnData;      
        private void Awake()
        {
            foreach(StartSpawnData i in startSpawnData)
                i.AddToPlayer(transform.position);
            Destroy(gameObject);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position, 1);
        }
    }

    [Serializable]
    public struct StartSpawnData
    {
        public int[] players;
        public FactionProperties[] factions;
        public GameEntityPool entity;
        public void AddToPlayer(Vector3 position)
        {
            StartEntityData data = new StartEntityData
            {
                entity = entity,
                offsetPosition = position
            };
            foreach (int i in players)
            {
                if (PlayerContext.players.Length > i && Array.IndexOf(factions, PlayerContext.players[i].faction) > -1)
                    PlayerContext.players[i].startEntities.Add(data);
            }
        }
    }

}
