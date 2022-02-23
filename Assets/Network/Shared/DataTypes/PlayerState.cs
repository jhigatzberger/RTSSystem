using System;
using Unity.Netcode;

namespace JHiga.RTSEngine.Network
{
    public struct PlayerState : INetworkSerializable, IEquatable<PlayerState>
    {
        public enum ReplaceType
        {
            ClientId,
            FactionId,
            Team,
            Status
        }

        public short PlayerId => (short)NetworkGameManager.ClientToPlayer(clientId);
        public short Team => RTSWorldData.Instance.playableTeams[playableTeamIndex];
        public ulong clientId;
        public short factionId;
        public short playableTeamIndex;
        public PlayerStatus status;
        public SkinnedPlayer Skin => new SkinnedPlayer(PlayerId, Team, factionId);
        public static PlayerState Update(PlayerState original, object value, ReplaceType replaceType)
        {
            return new PlayerState(original, value, replaceType);
        }
        public PlayerState(PlayerState original, object value, ReplaceType replaceType)
        {
            clientId = replaceType == ReplaceType.ClientId ? (ulong)value : original.clientId;
            factionId = replaceType == ReplaceType.FactionId ? (short)value : original.factionId;
            status = replaceType == ReplaceType.Status ? (PlayerStatus)value : original.status;
            playableTeamIndex = replaceType == ReplaceType.Team ? (short)value : original.playableTeamIndex;
        }
        public bool Equals(PlayerState other)
        {
            return other.clientId.Equals(clientId) && other.factionId.Equals(factionId) && other.playableTeamIndex.Equals(playableTeamIndex);
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientId);
            serializer.SerializeValue(ref factionId);
            serializer.SerializeValue(ref playableTeamIndex);
            serializer.SerializeValue(ref status);
        }
        public override string ToString()
        {
            return $"PlayerState {clientId}: {factionId} {playableTeamIndex} {status}";
        }
    }
}
