using System;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class GameEntityPool : ScriptableObject
    {
        public GameObject prefab;
        public ExtensionFactory[] properties;
        private Dictionary<Type, int> _extensionMap;
        private Dictionary<Type, int> ExtensionMap
        {
            get
            {
                if(_extensionMap == null)
                {
                    _extensionMap = new Dictionary<Type, int>();
                    for (int i = 0; i < properties.Length; i++)
                        _extensionMap.Add(properties[i].ExtensionType, i);
                }
                return _extensionMap;
            }
        }
        private readonly GameEntity[] _entities = new GameEntity[UIDConstants.MAX_POOL_SIZE];
        private HashSet<int> pendingPoolIds = new HashSet<int>();
        public static GameEntityPool Get(UID uid)
        {
            return PlayerData.Get(uid).factories[uid.poolIndex];
        }
        public static GameEntityPool Get(int uid)
        {
            return PlayerData.Get(uid).factories[UID.GetPoolIndex(uid)];
        }
        public static GameEntityPool CopyForPlayer(GameEntityPool original, int playerId, ExtensionFactory[] uniqueExtensionFactories)
        {
            GameEntityPool factory = Instantiate(original);
            factory.PlayerId = playerId;
            factory.properties = uniqueExtensionFactories;
            return factory;
        }
        public int PlayerId { get; private set; }
        public int Index { get; internal set; }
        public GameEntity[] entities => _entities;
        public GameEntity Spawn(Vector3 position, UID uid)
        {
            GameEntity entity = entities[uid.entityIndex];
            if (entity != null)
            {
                entity.gameObject.SetActive(true);
                entity.transform.position = position;
                entity.enabled = true;
            }
            else
            {
                entity = Instantiate(prefab, position, Quaternion.identity).AddComponent<GameEntity>();
                entity.extensionMap = ExtensionMap;
                entity.Extensions = Build(entity);
                entities[uid.entityIndex] = entity;
            }
            entity.UniqueID = uid;
            pendingPoolIds.Remove(uid.entityIndex);
            return entity;
        }
        public int GenerateEntityID()
        {
            for (int i = 0; i < entities.Length; i++)
            {                
                if ((entities[i] == null || !entities[i]) && !pendingPoolIds.Contains(i))
                {
                    pendingPoolIds.Add(i);
                    return i;
                }
            }
            return -1;            
        }
        private IEntityExtension[] Build(IExtendableEntity entity)
        {
            IEntityExtension[] extensions = new IEntityExtension[properties.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = properties[i].Build(entity);
            return extensions;
        }
    }
}
