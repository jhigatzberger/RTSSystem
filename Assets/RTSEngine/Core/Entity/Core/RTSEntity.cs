using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class RTSEntity : ScriptableObject
    {
        public GameObject prefab;
        public RTSProperty[] properties;

        private readonly List<RTSBehaviour> activePool = new List<RTSBehaviour>();
        private readonly Queue<RTSBehaviour> inactivePool = new Queue<RTSBehaviour>();

        public IExtension[] Build(RTSBehaviour behaviour)
        {
            IExtension[] extensions = new RTSExtension[properties.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = properties[i].Build(behaviour);
            return extensions;
        }

        public RTSBehaviour Instantiate(Vector3 position, int id, int team)
        {
            RTSBehaviour entity;
            if (inactivePool.Count == 0)
                entity = Instantiate(prefab, position, Quaternion.identity).GetComponent<RTSBehaviour>();
            else
            {
                entity = inactivePool.Dequeue();
                entity.transform.position = position;
                entity.enabled = true;
            }
            entity.Spawn(this, id, team);
            activePool.Add(entity);
            return entity;
        }
        public void Return(RTSBehaviour behaviour)
        {
            activePool.Remove(behaviour);
            inactivePool.Enqueue(behaviour);
        }
    }

}
