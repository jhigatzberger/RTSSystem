using System;

namespace JHiga.RTSEngine
{
    public static class PlayerContext
    {
        public static int PlayerId { get; set; }
        public static PlayerData[] players;
        public static bool AreEnenmies(int p1, int p2)
        {
            return players[p1].team != players[p2].team;
        }
    }
}
