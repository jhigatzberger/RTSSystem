using System;
using Unity.Netcode;

namespace JHiga.RTSEngine.Network
{
    public struct PlayerState : INetworkSerializable, IEquatable<PlayerState>
    {
        public ulong clientId;
        public short factionId;
        public short team;
        public PlayerStatus status;

        public bool Equals(PlayerState other)
        {
            return other.clientId.Equals(clientId) && other.factionId.Equals(factionId) && other.team.Equals(team);
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientId);
            serializer.SerializeValue(ref factionId);
            serializer.SerializeValue(ref team);
        }
    }
}
