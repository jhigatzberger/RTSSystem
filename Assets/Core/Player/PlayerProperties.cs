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
        public LayerMask ownLayer;
        public LayerMask enemyLayer;
        public FactionProperties faction;
        public GameEntityPool[] factories;
        public List<StartEntityData> startEntities;
        public PlayerProperties(SkinnedPlayer skinnedPlayer)
        {
            id = skinnedPlayer.id;
            team = skinnedPlayer.team;
            color = RTSWorldData.Instance.playerColors[id];
            ownLayer = RTSWorldData.Instance.teamLayers[team-1];
            enemyLayer = GenerateEnemyLayer();
            faction = RTSWorldData.Instance.playableFactions[skinnedPlayer.faction];
            factories = faction.CopyEntities(id);
            startEntities = new List<StartEntityData>(faction.startEntities);
        }
        private PlayerProperties(RTSWorldData data)
        {
            id = 0;
            team = data.mapTeam;
            color = data.playerColors[0];
            ownLayer = 1;
            enemyLayer = 0;
            faction = data.mapFaction;
            factories = data.mapFaction.CopyEntities(0);
            startEntities = new List<StartEntityData>(data.mapFaction.startEntities);
        }
        public static PlayerProperties GenerateGaia(RTSWorldData data)
        {
            return new PlayerProperties(data);
        }
        private LayerMask GenerateEnemyLayer()
        {
            int layer = 0;
            for (int i = 0; i < RTSWorldData.Instance.teamLayers.Length; i++)
            {
                if (RTSWorldData.Instance.playableTeams[i] != team)
                    layer |= 1 << RTSWorldData.Instance.teamLayers[i];
            }
            return layer;
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
