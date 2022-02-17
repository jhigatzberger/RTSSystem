using JHiga.RTSEngine.CommandPattern;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public struct NetworkSerializableCommandData : INetworkSerializable
    {
        public ushort commandId;
        public bool clearQueueOnEnqueue;
        public SkinnedTarget target;
        public int[] entities;

        public NetworkSerializableCommandData (SkinnedCommand command)
        {
            commandId = command.commandId;
            clearQueueOnEnqueue = command.references.clearQueueOnEnqueue;
            target = command.references.target;
            entities = command.references.entities;
        }

        public SkinnedCommand Skin
        {
            get
            {
                return new SkinnedCommand
                {
                    commandId = commandId,
                    references = new SkinnedCommandReferences
                    {
                        clearQueueOnEnqueue = clearQueueOnEnqueue,
                        entities = entities,
                        target = target
                    }
                };
            }
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref commandId);
            serializer.SerializeValue(ref clearQueueOnEnqueue);
            serializer.SerializeValue(ref target);
            
            int length = 0;
            if (!serializer.IsReader)
                length = entities.Length;

            serializer.SerializeValue(ref length);

            if (serializer.IsReader)
                entities = new int[length];

            for (int n = 0; n < length; ++n)
                serializer.SerializeValue(ref entities[n]);
        }
    }
}
