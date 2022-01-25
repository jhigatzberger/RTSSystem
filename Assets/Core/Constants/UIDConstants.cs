using System;

namespace JHiga.RTSEngine
{
    // example UID for player 1 (2nd player starting from 0 - 0 being gaia/mother nature/the map), second type of entity (pool index), third entity of that type spawned:
    // 1002003
    // example UID for player 8 (9th player), 1000th type of entity (pool index), 1000th entity of that type spawned:
    // 9999999    
    public static class UIDConstants
    {
        public const int MAX_PLAYERS = 9; // Player 0 = GAIA!
        public const int MAX_POOL_SIZE = 1000;
        public const int MAX_POOLS = 1000;

        public static readonly int FIRST_PLAYER_ID =
            RoundUp(MAX_PLAYERS)* 
            RoundUp(MAX_POOLS) *
            RoundUp(MAX_POOL_SIZE);

        public static readonly int FIRST_POOL_ID =
            RoundUp(MAX_PLAYERS) *
            RoundUp(MAX_POOLS);

        private static int RoundUp(int i)
        {
            return ((int)Math.Ceiling(i / 10.0)) * 10;
        }
    }    
}

