namespace JHiga.RTSEngine
{
    [System.Serializable]
    public struct UID
    {
        public int unique;
        public int player;
        public int pool;
        public int entity;
        public UID(int uniqueId)
        {
            this.unique = uniqueId;
            player = GetPlayerIndex(uniqueId);
            pool = GetPoolIndex(uniqueId);
            entity = GetEntityIndex(uniqueId);
        }
        public UID(int playerIndex, int poolIndex, int entityIndex)
        {
            this.player = playerIndex;
            this.pool = poolIndex;
            this.entity = entityIndex;
            unique = UIDConstants.FIRST_PLAYER_ID * playerIndex + UIDConstants.FIRST_POOL_ID * poolIndex + entityIndex;
        }
        public static int PlayerShifted(int uniqueId, int player)
        {
            return uniqueId - (GetPlayerIndex(uniqueId) - player) * UIDConstants.FIRST_PLAYER_ID;
        }
        public static int PoolShifted(int uniqueId, int pool)
        {
            return uniqueId - (GetPoolIndex(uniqueId) - pool) * UIDConstants.FIRST_POOL_ID;
        }
        public static int EntityShifted(int uniqueId, int entity)
        {
            return uniqueId - (GetEntityIndex(uniqueId) - entity);
        }
        public static int GetPlayerIndex(int uniqueId)
        {
            return uniqueId / UIDConstants.FIRST_PLAYER_ID;
        }
        public static int GetPoolIndex(int uniqueId)
        {
            return (uniqueId - GetPlayerIndex(uniqueId) * UIDConstants.FIRST_PLAYER_ID) / UIDConstants.FIRST_POOL_ID;
        }
        public static int GetEntityIndex(int uniqueId)
        {
            return uniqueId - GetPlayerIndex(uniqueId) * UIDConstants.FIRST_PLAYER_ID - GetPoolIndex(uniqueId) * UIDConstants.FIRST_POOL_ID;
        }
    }
}