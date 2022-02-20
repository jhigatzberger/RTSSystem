using System;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class ResourceClient : MonoBehaviour
    {
        public static ResourceClient Instance;
        private Action[] callbacks = new Action[500];
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ResourceEvents.OnAlterResourceRequest += ResourceEvents_OnAlterResourceRequest;
        }

        private void ResourceEvents_OnAlterResourceRequest(AlterResourceRequest request)
        {
            int callbackId = -1;
            for (int i = 0; i < callbacks.Length; i++)
            {
                if (callbacks[i] == null)
                {
                    callbackId = i;
                    callbacks[i] = request.successCallback;
                    break;
                }
            }
            if (callbackId >= 0)
                ResourceNetwork.Instance.AlterResourceServerRpc(NetworkManager.Singleton.LocalClientId, new ResourceNetworkPayload(request.data, callbackId));
        }

        public void UpdateResource(ResourceNetworkPayload request, bool success)
        {
            Debug.Log("UpdateResource!");
            foreach(ResourceData data in request.data)
                ResourceEvents.UpdateResource(data);
            if (success)
                callbacks[request.successCallback]();
            callbacks[request.successCallback] = null;
        }
    }
    public struct ResourceNetworkPayload : INetworkSerializable
    {
        public ResourceData[] data;
        public int successCallback;
        public ResourceNetworkPayload(ResourceData[] data, int callback)
        {
            this.data = data;
            successCallback = callback;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref successCallback);

            int length = 0;
            if (!serializer.IsReader)
                length = data.Length;

            serializer.SerializeValue(ref length);

            if (serializer.IsReader)
                data = new ResourceData[length];

            for (int n = 0; n < length; ++n)
                serializer.SerializeValue(ref data[n]);
        }

    }
}
