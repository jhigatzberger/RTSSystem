using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [System.Serializable]
    public class PlayerProperties
    {
        public int id;
        public int team;
        public Color color;
        public string layerName;
        public LayerMask enemies;
        public FactionProperties faction;
        public GameEntityPool[] factories;
        public List<StartEntityData> startEntities;
        public PlayerProperties(SkinnedPlayer skinnedPlayer)
        {
            id = skinnedPlayer.id;
            team = skinnedPlayer.team;
            layerName = "Player" + id;
            enemies = GetEnemyLayer();
            faction = RTSWorldData.Instance.playableFactions[skinnedPlayer.faction];
            factories = faction.CopyEntities(id);
            startEntities = new List<StartEntityData>(faction.startEntities);
        }
        public LayerMask GetEnemyLayer()
        {
            Debug.LogWarning("TODO: implement enemy layermask generation");
            return 100;
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
    public struct SkinnedPlayer
    {
        public short id;
        public short team;
        public short faction;

        public SkinnedPlayer(short id, short team, short faction)
        {
            this.id = id;
            this.team = team;
            this.faction = faction;
        }
    }
}
