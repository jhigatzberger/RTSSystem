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
                ResourceNetwork.Instance.AlterResourceServerRpc(NetworkManager.Singleton.LocalClientId, request.resourceType, request.amount, callbackId);
        }

        public void UpdateResource(ResourceData data, int callbackId, bool success)
        {
            Debug.Log("UpdateResource!");
            ResourceEvents.UpdateResource(data);
            if (success)
                callbacks[callbackId]();
            callbacks[callbackId] = null;
        }
    }
}
