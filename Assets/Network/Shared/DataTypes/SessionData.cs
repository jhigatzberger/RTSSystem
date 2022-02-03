using Unity.Netcode;

namespace JHiga.RTSEngine.Network
{
    public struct SessionData : INetworkSerializable
    {
        public short mapIndex;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {            
            serializer.SerializeValue(ref mapIndex);
        }
    }
}