using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    public class StartEntityMarker : MonoBehaviour
    { 
        [SerializeField] private FactionProperties faction;
        [SerializeField] private EntityPool toSpawn;        
        private void Awake()
        {         
            StartEntityData data = new StartEntityData
            {
                entity = toSpawn,
                offsetPosition = transform.position
            };
            faction.startEntities.Add(data); 
            Destroy(gameObject);
        }
        private void OnDrawGizmos()
        {
            if(toSpawn != null && toSpawn.prefab != null)
            {
                foreach(MeshFilter mf in toSpawn.prefab.GetComponentsInChildren<MeshFilter>())
                    Gizmos.DrawWireMesh(mf.sharedMesh, -1, transform.position, prefab.transform.rotation, prefab.transform.localScale);
            }
        }

    }
}
