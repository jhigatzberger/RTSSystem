using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Faction", menuName = "RTS/Faction")]
    public class FactionProperties : ScriptableObject
    {
        public int startEntityIndex = 0;
        public EntityFactory<GameEntity>[] entities;
        /// <summary>
        /// Instantiates each factory to allow different properties for each player that do not persist after the match.
        /// Each exact same property however should only be instantiated once per player as to not waste memory.
        /// e.g.: Upgrades
        /// </summary>
        public EntityFactory<GameEntity>[] GetIndividualInstanceFactories(int playerId)
        {
            Dictionary<ExtensionFactory, ExtensionFactory> extensionFactoryReference = new Dictionary<ExtensionFactory, ExtensionFactory>();
            EntityFactory<GameEntity>[] factories = new EntityFactory<GameEntity>[entities.Length];
            for(int i = 0; i < entities.Length; i++)
            {
                //PooledGameEntityFactory factoryInstance = Instantiate(entities[i]);
                ExtensionFactory[] extensionFactories = new ExtensionFactory[entities[i].ExtensionFactories.Length];
                for(int ii = 0; ii < extensionFactories.Length; ii++)
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
                factories[i] = EntityFactory<GameEntity>.Copy(entities[i], playerId, i, extensionFactories);
            }
            return factories;
        }    
    
    }
}