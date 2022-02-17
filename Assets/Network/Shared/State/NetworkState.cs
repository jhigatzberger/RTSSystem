using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{

    public abstract class NetworkState : NetworkBehaviour
    {
        public enum State
        {
            None,
            Lobby,
            Loading,
            Game,
            PostGame
        }
        public abstract State Type { get; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            NetworkGameManager.Instance.Status = PlayerStatus.Pending;
        }
        public void Finish()
        {
            NetworkGameManager.Instance.Status = PlayerStatus.Ready;
        }
    }
}
