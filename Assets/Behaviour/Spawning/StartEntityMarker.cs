using System;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [ExecuteAlways]
    public class StartEntityMarker : MonoBehaviour
    {
        [SerializeField] private StartSpawnData[] startSpawnData;
        private void Awake()
        {
            if (Application.IsPlaying(gameObject))
            {
                foreach (StartSpawnData i in startSpawnData)
                    i.AddToPlayer(transform.position);
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if (!Application.IsPlaying(gameObject))
            {
                if (startSpawnData != null &&
                    startSpawnData.Length > 0 &&
                    startSpawnData[0].entity != null &&
                    startSpawnData[0].entity.prefabs != null &&
                    startSpawnData[0].entity.prefabs.Length > 0 &&
                    startSpawnData[0].entity.prefabs[0] != null &&
                    !transform.Find("___PREVIEW___"))
                {
                    GameObject preview = Instantiate(startSpawnData[0].entity.prefabs[0], transform);
                    preview.name = "___PREVIEW___";
                }
            }
        }

        private void OnDisable()
        {
            if (!Application.IsPlaying(gameObject))
            {
                foreach (Transform t in transform)
                    DestroyImmediate(t.gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
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
                Debug.Log(PlayerContext.players[i].faction.name);
                if(PlayerContext.players.Length > i && Array.IndexOf(factions, PlayerContext.players[i].faction) > -1)
                    PlayerContext.players[i].startEntities.Add(data);
            }
        }
    }
}
