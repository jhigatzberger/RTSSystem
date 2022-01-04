using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class PooledEntityFactory : EntityFactory
    {
        public GameObject prefab;
        private readonly List<PooledEntity> activePool = new List<PooledEntity>();
        private readonly Queue<PooledEntity> inactivePool = new Queue<PooledEntity>();
        public ExtensionFactory[] properties;
        public override IExtensionFactory[] ComponentFactories => properties;
        public PooledEntity Spawn(Vector3 position, int id, int team)
        {
            PooledEntity entity;
            if (inactivePool.Count == 0)
            {
                entity = Instantiate(prefab, position, Quaternion.identity).GetComponent<PooledEntity>();
                entity.ScriptableComponents = Build(entity);
            }
            else
            {
                entity = inactivePool.Dequeue();
                entity.transform.position = position;
                entity.enabled = true;
            }
            entity.Spawn(ReturnToPool, id, team);
            activePool.Add(entity);
            return entity;
        }
        public void ReturnToPool(PooledEntity behaviour)
        {
            activePool.Remove(behaviour);
            inactivePool.Enqueue(behaviour);
        }
    }

}
