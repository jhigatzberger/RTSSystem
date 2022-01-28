using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{

    public abstract class NetworkState : NetworkBehaviour
    {
        public abstract object GetData { get; }
        public GameObject serverPrefab;
        public GameObject clientPrefab;
        public enum State
        {
            Lobby,
            Loading,
            Game,
            PostGame
        }
        public abstract State Type { get; }
        public void CollectiveActive()
        {
            Debug.Log("enter: " + Type);
            NetworkGameManager.Instance.Status = ClientStatus.Active;
            OnCollectiveActive();
            DontDestroyOnLoad(gameObject);
            if (IsServer && serverPrefab != null)
                Instantiate(serverPrefab, transform);
            if (IsClient && clientPrefab != null)
                Instantiate(clientPrefab, transform);
        }
        public abstract void OnCollectiveActive();
        public void Exit()
        {
            OnExit();
            Destroy(gameObject);
        }
        public virtual void OnExit() { }
        public void Finish()
        {
            NetworkGameManager.Instance.Status = ClientStatus.Finished;
        }
    }
}
