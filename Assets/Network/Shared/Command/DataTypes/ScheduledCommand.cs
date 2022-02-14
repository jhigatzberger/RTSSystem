using Unity.Netcode;

namespace JHiga.RTSEngine.Network
{
    public struct ScheduledCommand : INetworkSerializable
    {
        public NetworkSerializableCommandData command;
        public ulong scheduledStep;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref scheduledStep);
            serializer.SerializeNetworkSerializable(ref command);
        }
    }
}