using System;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public class StartEntityMarker : MonoBehaviour
    {
        [SerializeField] private FactionProperties[] factions;
        [SerializeField] private int[] players;
        [SerializeField] private GameEntityPool toSpawn;        
        private void Awake()
        {         
            StartEntityData data = new StartEntityData
            {
                entity = toSpawn,
                offsetPosition = transform.position
            };
            foreach(int i in players)
            {   
                if (PlayerContext.players.Length > i && Array.IndexOf(factions, PlayerContext.players[i].faction) > -1)
                    PlayerContext.players[i].StartEntities.Add(data);
            }
            Destroy(gameObject);
        }
        private void OnDrawGizmos()
        {
            if(toSpawn != null && toSpawn.prefab != null)
            {
                foreach(MeshFilter mf in toSpawn.prefab.GetComponentsInChildren<MeshFilter>())
                    Gizmos.DrawWireMesh(mf.sharedMesh, -1, transform.position, toSpawn.prefab.transform.rotation, toSpawn.prefab.transform.localScale);
            }
        }
    }
}
