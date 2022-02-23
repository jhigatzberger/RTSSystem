using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [System.Serializable]
    public class PlayerData
    {
        public int id;
        public int team;
        public Color color;
        public int ownMask;
        public int enemyMask;
        public FactionProperties faction;
        public GameEntityPool[] factories;
        public List<StartEntityData> startEntities;
        public PlayerData(SkinnedPlayer skinnedPlayer)
        {
            id = skinnedPlayer.id;
            team = skinnedPlayer.team;
            color = RTSWorldData.Instance.playerColors[id];
            ownMask = RTSWorldData.Instance.teamLayers[team-1];
            enemyMask = GenerateEnemyMask();
            faction = RTSWorldData.Instance.playableFactions[skinnedPlayer.faction];
            factories = faction.CopyEntities(id);
            startEntities = new List<StartEntityData>(faction.startEntities);
        }
        private PlayerData()
        {
            RTSWorldData data = RTSWorldData.Instance;
            id = 0;
            team = data.mapTeam;
            color = data.playerColors[0];
            ownMask = 0;
            enemyMask = GenerateEnemyMask();
            faction = data.mapFaction;
            factories = data.mapFaction.CopyEntities(0);
            startEntities = new List<StartEntityData>(data.mapFaction.startEntities);
        }
        public static PlayerData GenerateGaia()
        {
            return new PlayerData();
        }
        private int GenerateEnemyMask()
        {
            int layer = 0;
            for (int i = 0; i < RTSWorldData.Instance.teamLayers.Length; i++)
            {
                if (RTSWorldData.Instance.playableTeams[i] != team)
                    layer |= 1 << RTSWorldData.Instance.teamLayers[i];
            }
            return layer;
        }      
        public static PlayerData Get(UID uid)
        {
            return PlayerContext.players[uid.player];
        }
        public static PlayerData Get(int uid)
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
