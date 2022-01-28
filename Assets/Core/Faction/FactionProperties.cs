using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Faction", menuName = "RTS/Faction")]
    public class FactionProperties : ScriptableObject
    {
        public short id;
        public List<StartEntityData> startEntities = new List<StartEntityData>();
        public List<EntityGroup> entityGroups = new List<EntityGroup>();

        /// <summary>
        /// Instantiates each factory to allow different properties for each player that do not persist after the match.
        /// Each exact same property however should only be instantiated once per player as to not waste memory.
        /// e.g.: Upgrades
        /// </summary>
        public GameEntityPool[] CopyEntities(int playerId)
        {
            Dictionary<ExtensionFactory, ExtensionFactory> extensionFactoryReference = new Dictionary<ExtensionFactory, ExtensionFactory>();
            List<GameEntityPool> entities = GetAllEntitiesFromAllGroups();
            GameEntityPool[] factories = new GameEntityPool[entities.Count];
            for (int i = 0; i < entities.Count; i++)
            {
                ExtensionFactory[] extensionFactories = new ExtensionFactory[entities[i].properties.Length];
                for (int ii = 0; ii < extensionFactories.Length; ii++)
                {
                    if (extensionFactoryReference.TryGetValue(entities[i].properties[ii], out ExtensionFactory value))
                        extensionFactories[ii] = value;
                    else
                    {
                        ExtensionFactory extension = Instantiate(entities[i].properties[ii]);
                        extensionFactoryReference.Add(entities[i].properties[ii], extension);
                        extensionFactories[ii] = extension;
                    }
                }
                factories[i] = GameEntityPool.CopyForPlayer(entities[i], playerId, extensionFactories);
            }
            return factories;
        }
        private List<GameEntityPool> GetAllEntitiesFromAllGroups()
        {
            int index = 0;
            List<GameEntityPool> entities = new List<GameEntityPool>();
            foreach (EntityGroup group in entityGroups)
            {
                foreach (GameEntityPool factory in group.GetAllEntities())
                {
                    factory.Index = index++;
                    entities.Add(factory);
                }
            }
            return entities;
        }
    }

    [System.Serializable]
    public class StartEntityData
    {
        public GameEntityPool entity;
        public Vector3 offsetPosition;
    }
}