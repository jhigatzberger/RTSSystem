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
        private GameEntityFactory[] _factories;
        public GameEntityFactory[] Factories
        {
            get
            {
                if (_factories == null)
                    _factories = faction.GetIndividualInstanceFactories(id);
                return _factories;
            } 
        }
        public static PlayerProperties Get(UID uid)
        {
            return PlayerContext.players[uid.playerIndex];
        }
    }
}