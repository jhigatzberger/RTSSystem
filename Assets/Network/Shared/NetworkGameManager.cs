using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{

    public class NetworkGameManager : NetworkBehaviour
    {
        public static NetworkGameManager Instance { get; private set; }

        public NetworkVariable<SessionData> sessionData = new NetworkVariable<SessionData>();
        public NetworkList<PlayerState> playerData;
        public NetworkVariable<NetworkState.State> currentState = new NetworkVariable<NetworkState.State>();
        [SerializeField] private NetworkState[] states;
        private Dictionary<NetworkState.State, NetworkState> stateMap;
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                stateMap = new Dictionary<NetworkState.State, NetworkState>();
                foreach (NetworkState entry in states)
                    stateMap.Add(entry.Type, entry);
                NetworkManager.OnClientDisconnectCallback += OnClientDisconnect;
                NetworkManager.OnClientConnectedCallback += OnClientConnect;
                OnClientConnect(OwnerClientId);
                ChangeStateServerRpc(NetworkState.State.Lobby);
            }
        }

        private void OnClientConnect(ulong client)
        {
            playerData.Add(new PlayerState
            {
                clientId = client,
                team = 0,
                factionId = 0,
                status = PlayerStatus.Pending
            });
        }

        public override void OnNetworkDespawn()
        {
            NetworkManager.OnClientDisconnectCallback -= OnClientDisconnect;
            NetworkManager.OnClientConnectedCallback -= OnClientConnect;
        }

        private void OnClientDisconnect(ulong client)
        {
            for (int i = 0; i < playerData.Count; i++)
            {
                if (playerData[i].clientId == client)
                {
                    playerData.RemoveAt(i);
                    return;
                }
            }
        }
        [ServerRpc]
        private void SetStatusServerRpc(PlayerStatus status, ServerRpcReceiveParams receiveParams = default)
        {
            for (int i = 0; i < playerData.Count; i++)
            {
                if (playerData[i].clientId == receiveParams.SenderClientId)
                    playerData[i] = new PlayerState
                    {
                        status = status,
                        clientId = playerData[i].clientId,
                        factionId = playerData[i].factionId,
                        team = playerData[i].team
                    };
            }
            
            PlayerStatus c_Status = CollectiveStatus;
            if (status > c_Status)
                return;
            switch (c_Status)
            {
                case PlayerStatus.Ready:
                    ChangeStateServerRpc((NetworkState.State)(((int)currentState.Value + 1) % Enum.GetValues(typeof(NetworkState.State)).Length));
                    break;
            }
        }
        public PlayerStatus Status
        {
            get
            {
                for (int i = 0; i < playerData.Count; i++)
                {
                    if (playerData[i].clientId == NetworkManager.LocalClientId)
                        return playerData[i].status;
                }
                return PlayerStatus.Pending;
            }            
            set
            {
                SetStatusServerRpc(value);
            }
        }
        public PlayerStatus CollectiveStatus
        {
            get
            {
                PlayerStatus lowestStatus = PlayerStatus.Finished;
                foreach (PlayerState p in playerData)
                    if (p.status < lowestStatus)
                        lowestStatus = p.status;
                return lowestStatus;
            }
        }
        [ServerRpc]
        public void ChangeStateServerRpc(NetworkState.State stateType)
        {
            Debug.Log(currentState.Value + " ChangeStateServerRpc " + stateType.ToString());
            if (currentState.Value != NetworkState.State.None)
            {
                if (stateType == currentState.Value)
                    return;
                Debug.Log("Despawn");
                FindObjectOfType<NetworkState>().GetComponent<NetworkObject>().Despawn();
            }
            currentState.Value = stateType;
            NetworkState newState = Instantiate(stateMap[stateType].gameObject).GetComponent<NetworkState>();
            newState.GetComponent<NetworkObject>().Spawn();
        }
    }
}