using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class PooledGameEntityFactory : InteractableFactory<GameEntity>
    {
        public GameObject prefab;
        private readonly List<GameEntity> activePool = new List<GameEntity>();
        private readonly Queue<GameEntity> inactivePool = new Queue<GameEntity>();
        public ExtensionFactory[] properties;
        public override ExtensionFactory[] ExtensionFactories => properties;
        public override GameEntity Spawn(Vector3 position, int id, int playerId)
        {
            Debug.Log(name + " spawn " + playerId);
            foreach (PooledGameEntityFactory f in PlayerContext.players[playerId].Factories.Keys)
                Debug.Log(f.name);
            // call the spawn function on the factory that belongs to that player
            // each player has its own factory for each unit to allow us to have upgrades via ExtensionFactory properties
            PooledGameEntityFactory factory = PlayerContext.players[playerId].Factories[this];
                return factory.DoSpawn(position, id);
        }

        private GameEntity DoSpawn(Vector3 position, int id)
        {
            Debug.Log("dospawn " + id);
            GameEntity entity;
            if (inactivePool.Count == 0)
            {
                entity = Instantiate(prefab, position, Quaternion.identity).GetComponent<GameEntity>();
                entity.Extensions = Build(entity);
            }
            else
            {
                entity = inactivePool.Dequeue();
                entity.transform.position = position;
                entity.enabled = true;
            }
            entity.Spawn(ReturnToPool, id, PlayerContext.PlayerId);
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
