using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class GameEntityPool : ScriptableObject
    {
        public static GameEntityPool Get(UID uid)
        {
            return PlayerProperties.Get(uid).Factories[uid.poolIndex];
        }
        public static GameEntityPool Get(int uid)
        {
            return PlayerProperties.Get(UID.GetPlayerIndex(uid)).Factories[UID.GetPoolIndex(uid)];
        }
        public static GameEntityPool CopyForPlayer(GameEntityPool original, int playerId, ExtensionFactory[] uniqueExtensionFactories)
        {
            GameEntityPool factory = Instantiate(original);
            factory.PlayerId = playerId;
            factory.ExtensionFactories = uniqueExtensionFactories;
            return factory;
        }
        public GameObject prefab;
        public ExtensionFactory[] ExtensionFactories { get; set; }
        public int PlayerId { get; private set; }
        public int Index { get; internal set; }
        public GameEntity[] Entities => _entities;
        private readonly GameEntity[] _entities = new GameEntity[UIDConstants.MAX_POOL_SIZE];
        public GameEntity Spawn(Vector3 position, UID uid)
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
        public int GenerateEntityID()
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
        public IEntityExtension[] Build(IExtendableEntity entity)
        {
            IEntityExtension[] extensions = new IEntityExtension[ExtensionFactories.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = ExtensionFactories[i].Build(entity);
            return extensions;
        }
    }
}
