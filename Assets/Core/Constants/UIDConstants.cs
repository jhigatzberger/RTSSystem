
using JHiga.RTSEngine.Spawning;
using System;

namespace JHiga.RTSEngine
{
    public static class UIDConstants
    {
        public const int MAX_PLAYERS = 8;
        public const int MAX_POOL_SIZE = 999;
        public const int MAX_POOLS = 999;

        public static readonly int FIRST_PLAYER_ID =
            RoundUp(MAX_PLAYERS + 1)* 
            RoundUp(MAX_POOLS + 1) *
            RoundUp(MAX_POOL_SIZE + 1);

        public static readonly int FIRST_POOL_ID =
            RoundUp(MAX_PLAYERS + 1) *
            RoundUp(MAX_POOLS + 1);

        private static int RoundUp(int i)
        {
            return ((int)Math.Ceiling(i / 10.0)) * 10;
        }
    }    
}

