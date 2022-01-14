using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class PooledGameEntityFactory : EntityFactory<GameEntity>
    {
        public GameObject prefab;
        private readonly GameEntity[] entities = new GameEntity[EntityConstants.MAX_POOL_SIZE];
        public ExtensionFactory[] properties;
        public override ExtensionFactory[] ExtensionFactories
        {
            get
            {
                return properties;
            }
            protected set
            {
                properties = value;
            }
        }
        public override GameEntity Spawn(Vector3 position, int id)
        {
            GameEntity entity = entities[id];
            if (entity != null)
            {
                entity.transform.position = position;
                entity.enabled = true;
            }
            else
            {
                entity = Instantiate(prefab, position, Quaternion.identity).GetComponent<GameEntity>();
                entity.Extensions = Build(entity);
                entities[id] = entity;
                Debug.Log("SAVING " + entity + " " + id);
            }            
            entity.EntityId = new UID(PlayerId, Index, id);
            return entity;
        }

        public override GameEntity Get(int index)
        {
            Debug.Log(index +" isnull: " + (entities[index] == null));
            return entities[index];
        }

        public override int GenerateEntityID()
        {
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] == null)
                    return i;
                if (!entities[i].enabled)
                    return i;
            }
            return -1;            
        }
    }
}
