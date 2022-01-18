using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Faction", menuName = "RTS/Faction")]
    public class FactionProperties : ScriptableObject
    {
        public StartEntityData[] startEntities; // fix spawning for this
        public List<EntityGroup> entityGroups = new List<EntityGroup>();
        /// <summary>
        /// Instantiates each factory to allow different properties for each player that do not persist after the match.
        /// Each exact same property however should only be instantiated once per player as to not waste memory.
        /// e.g.: Upgrades
        /// </summary>
        public GameEntityFactory[] CopyEntities(int playerId)
        {
            Dictionary<ExtensionFactory, ExtensionFactory> extensionFactoryReference = new Dictionary<ExtensionFactory, ExtensionFactory>();
            List<GameEntityFactory> entities = GetEntitiesFromAllGroups();
            GameEntityFactory[] factories = new GameEntityFactory[entities.Count];
            for (int i = 0; i < entities.Count; i++)
            {
                ExtensionFactory[] extensionFactories = new ExtensionFactory[entities[i].ExtensionFactories.Length];
                for (int ii = 0; ii < extensionFactories.Length; ii++)
                {
                    if (extensionFactoryReference.TryGetValue(entities[i].ExtensionFactories[ii], out ExtensionFactory value))
                        extensionFactories[ii] = value;
                    else
                    {
                        ExtensionFactory extension = Instantiate(entities[i].ExtensionFactories[ii]);
                        extensionFactoryReference.Add(entities[i].ExtensionFactories[ii], extension);
                        extensionFactories[ii] = extension;
                    }
                }
                factories[i] = GameEntityFactory.CopyForPlayer(entities[i], playerId, extensionFactories);
            }
            return factories;
        }
        private List<GameEntityFactory> GetEntitiesFromAllGroups()
        {
            int index = 0;
            List<GameEntityFactory> entities = new List<GameEntityFactory>();
            foreach (EntityGroup group in entityGroups)
            {
                foreach (GameEntityFactory factory in group.entities)
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
        public GameEntityFactory entity;
        public Vector3 offsetPosition;
    }
}