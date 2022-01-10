using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [System.Serializable]
    public class PlayerProperties
    {
        public int id;
        public int team;
        public string layerName;
        public Color color;
        public LayerMask enemies;
        public FactionProperties faction;
        private Dictionary<PooledGameEntityFactory, PooledGameEntityFactory> _factories;
        public Dictionary<PooledGameEntityFactory, PooledGameEntityFactory> Factories
        {
            get
            {
                if (_factories == null)
                    _factories = faction.GetIndividualInstanceFactories();
                return _factories;
            } 
        }
    }
}