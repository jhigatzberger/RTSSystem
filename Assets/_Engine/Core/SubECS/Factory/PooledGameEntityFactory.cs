using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class PooledGameEntityFactory : EntityFactory
    {
        public GameObject prefab;
        private readonly List<GameEntity> activePool = new List<GameEntity>();
        private readonly Queue<GameEntity> inactivePool = new Queue<GameEntity>();
        public ExtensionFactory[] properties;
        public override IExtensionFactory[] ComponentFactories => properties;
        public GameEntity Spawn(Vector3 position, int id, int team)
        {
            GameEntity entity;
            if (inactivePool.Count == 0)
            {
                entity = Instantiate(prefab, position, Quaternion.identity).GetComponent<GameEntity>();
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
        public void ReturnToPool(GameEntity behaviour)
        {
            activePool.Remove(behaviour);
            inactivePool.Enqueue(behaviour);
        }
    }

}
