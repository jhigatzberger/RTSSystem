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
        private GameEntityPool[] _factories;
        public GameEntityPool[] Factories
        {
            get
            {
                if (_factories == null)
                    _factories = faction.CopyEntities(id);
                return _factories;
            }
        }
        public static PlayerProperties Get(UID uid)
        {
            return PlayerContext.players[uid.playerIndex];
        }
        public static PlayerProperties Get(int uid)
        {
            return PlayerContext.players[UID.GetPlayerIndex(uid)];
        }
    }
}