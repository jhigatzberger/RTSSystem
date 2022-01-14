namespace JHiga.RTSEngine
{
    [System.Serializable]
    public struct UID
    {
        public int uniqueId;
        public int playerIndex;
        public int poolIndex;
        public int entityIndex;
        public UID(int uniqueId)
        {
            this.uniqueId = uniqueId;
            playerIndex = uniqueId / UIDConstants.FIRST_PLAYER_ID;
            poolIndex = (uniqueId - playerIndex * UIDConstants.FIRST_PLAYER_ID) / UIDConstants.FIRST_POOL_ID;
            entityIndex = (uniqueId - playerIndex * UIDConstants.FIRST_PLAYER_ID) - poolIndex * UIDConstants.FIRST_POOL_ID;

            playerIndex--;
            poolIndex--;
        }

        public UID(int playerIndex, int poolIndex, int entityIndex)
        {
            this.playerIndex = playerIndex;
            this.poolIndex = poolIndex;
            this.entityIndex = entityIndex;
            uniqueId = UIDConstants.FIRST_PLAYER_ID * (playerIndex+1) + UIDConstants.FIRST_POOL_ID * (poolIndex+1) + entityIndex;
        }

        public override string ToString()
        {
            return "U: " + uniqueId + " P " + playerIndex + " L " + poolIndex + " E " + entityIndex;
        }
    }
}