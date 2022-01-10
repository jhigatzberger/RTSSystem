using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Faction", menuName = "RTS/Faction")]
    public class FactionProperties : ScriptableObject
    {
        public PooledGameEntityFactory startEntity;
        public PooledGameEntityFactory[] entities;
        /// <summary>
        /// Instantiates each factory to allow different properties for each player that do not persist after the match.
        /// Each exact same property however should only be instantiated once per player as to not waste memory.
        /// e.g.: Upgrades
        /// </summary>
        public Dictionary<PooledGameEntityFactory, PooledGameEntityFactory> GetIndividualInstanceFactories()
        {
            Dictionary<ExtensionFactory, ExtensionFactory> extensionFactoryReference = new Dictionary<ExtensionFactory, ExtensionFactory>();
            Dictionary<PooledGameEntityFactory, PooledGameEntityFactory> typeToFactory = new Dictionary<PooledGameEntityFactory, PooledGameEntityFactory>();
            foreach(PooledGameEntityFactory factory in entities)
            {
                PooledGameEntityFactory factoryInstance = Instantiate(factory);
                ExtensionFactory[] extensionFactories = new ExtensionFactory[factoryInstance.ExtensionFactories.Length];
                for(int i = 0; i < extensionFactories.Length; i++)
                {
                    if (extensionFactoryReference.TryGetValue(factoryInstance.ExtensionFactories[i], out ExtensionFactory value))
                        extensionFactories[i] = value;
                    else
                    {
                        extensionFactoryReference.Add(factoryInstance.ExtensionFactories[i], Instantiate(factoryInstance.ExtensionFactories[i]));
                        extensionFactories[i] = factoryInstance.ExtensionFactories[i];
                    }
                }
                factoryInstance.properties = extensionFactories;
                typeToFactory.Add(factory, factoryInstance);
            }
            return typeToFactory;
        }    
    
    }
}