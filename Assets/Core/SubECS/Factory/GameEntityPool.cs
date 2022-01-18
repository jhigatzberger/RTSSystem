using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class GameEntityPool : GameEntityFactory
    {
        public GameObject prefab;

        public override GameEntity[] Entities => _entities;
        private readonly GameEntity[] _entities = new GameEntity[UIDConstants.MAX_POOL_SIZE];

        public override ExtensionFactory[] ExtensionFactories { get; set; }

        public override GameEntity Spawn(Vector3 position, UID uid)
        {
            GameEntity entity = Entities[uid.entityIndex];
            if (entity != null)
            {
                entity.transform.position = position;
                entity.enabled = true;
            }
            else
            {
                entity = Instantiate(prefab, position, Quaternion.identity).AddComponent<GameEntity>();
                entity.Extensions = Build(entity);
                Entities[uid.entityIndex] = entity;
            }
            entity.UniqueID = uid;
            return entity;
        }
        public override int GenerateEntityID()
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] == null)
                    return i;
                if (!Entities[i].enabled)
                    return i;
            }
            return -1;            
        }
    }
}
