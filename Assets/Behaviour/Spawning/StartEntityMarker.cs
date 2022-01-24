using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [ExecuteAlways]
    public class StartEntityMarker : MonoBehaviour
    {
        public StartEntityData data;
        public FactionProperties faction;
        private FactionProperties parent;
        public GameEntityPool toSpawn;
        private void Awake()
        {
            if (Application.isPlaying)
                Destroy(gameObject);

            if (data == null)
            {
                data = new StartEntityData
                {
                    entity = toSpawn,
                    offsetPosition = transform.position
                };
            }
        }

        private void OnDestroy()
        {
            if (!Application.isPlaying)
                parent.startEntities.Remove(data);
        }

        private void OnDrawGizmosSelected()
        {
            if(parent != faction)
            {
                if (parent != null && parent.startEntities.Contains(data))
                    parent.startEntities.Remove(data);
                parent = faction;
                if (parent != null)
                    parent.startEntities.Add(data);
            }
            if (data.offsetPosition != transform.position)
                data.offsetPosition = transform.position;
            if (data.entity != toSpawn)
                data.entity = toSpawn;
        }

        private void OnDrawGizmos()
        {
            if(toSpawn != null && toSpawn.prefab != null)
                Gizmos.DrawWireMesh(toSpawn.prefab.GetComponentInChildren<MeshFilter>().sharedMesh, -1, transform.position, transform.rotation, transform.localScale);
        }

    }
}
