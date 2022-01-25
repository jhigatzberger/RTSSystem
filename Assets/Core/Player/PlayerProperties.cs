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
        private List<StartEntityData> _startEntites;
        public List<StartEntityData> StartEntities
        {
            get
            {
                if(_startEntites == null)
                    InitPlayer();
                return _startEntites;
            }
        }            
        private GameEntityPool[] _factories;
        public GameEntityPool[] Factories
        {
            get
            {
                if (_factories == null)
                    InitPlayer();
                return _factories;
            }
        }        
        private void InitPlayer()
        {
            _factories = faction.CopyEntities(id);
            Debug.Log("done: " + id + " " + _factories.Length);
            _startEntites = new List<StartEntityData>(faction.startEntities);            
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
