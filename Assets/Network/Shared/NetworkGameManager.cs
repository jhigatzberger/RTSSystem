using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    //https://docs.unity3d.com/Manual/UNetManager.html
    public class NetworkGameManager : NetworkBehaviour
    {
        #region Singleton
        public static NetworkGameManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null || !IsOwner)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }
        #endregion
        [SerializeField] private NetworkState[] states;
        private Dictionary<NetworkState.NetworkStateType, NetworkState> stateMap;
        private NetworkState currentState;
        public override void OnNetworkSpawn()
        {
            PlayerContext.PlayerId = (int)NetworkManager.LocalClientId + 1;
            stateMap = new Dictionary<NetworkState.NetworkStateType, NetworkState>();
            foreach (NetworkState entry in states)
                stateMap.Add(entry.Type, entry);
            ChangeState(NetworkState.NetworkStateType.LobbyState);
        }
        public void ChangeState(NetworkState.NetworkStateType stateType) // CLIENTRPC & SERVERRPC!
        {
            if(currentState != null)
            {
                if (stateType == currentState.Type)
                    return;
                Destroy(currentState.gameObject);
            }
            currentState = Instantiate(stateMap[stateType].gameObject).GetComponent<NetworkState>();
            currentState.GetComponent<NetworkObject>().Spawn();
        }
    }
}