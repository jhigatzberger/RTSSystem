
using System;

namespace JHiga.RTSEngine
{
    public static class UIDConstants
    {
        public static readonly int FIRST_PLAYER_ID =
            RoundUp(PlayerData.MAX_PLAYERS + 1)* 
            RoundUp(EntityConstants.MAX_POOLS + 1) *
            RoundUp(EntityConstants.MAX_POOL_SIZE + 1);

        public static readonly int FIRST_POOL_ID =
            RoundUp(PlayerData.MAX_PLAYERS + 1) *
            RoundUp(EntityConstants.MAX_POOLS + 1);
        private static int RoundUp(int i)
        {
            return ((int)Math.Ceiling(i / 10.0)) * 10;
        }
    }
    
}

