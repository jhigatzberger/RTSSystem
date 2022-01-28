using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public enum ClientStatus : byte
    {
        Pending,
        Waiting,
        Active,
        Finished
    }
    public class NetworkGameManager : NetworkBehaviour
    {
        #region Init
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
        public override void OnNetworkSpawn()
        {
            stateMap = new Dictionary<NetworkState.State, NetworkState>();
            stateData = new Dictionary<NetworkState.State, object>();
            foreach (NetworkState entry in states)
                stateMap.Add(entry.Type, entry);

            PlayerContext.PlayerId = (int)NetworkManager.LocalClientId + 1;
            NetworkManager.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
            clientStatusMap = new Dictionary<ulong, ClientStatus>();
            ChangeState(NetworkState.State.Lobby);
        }

        public override void OnNetworkDespawn()
        {
            NetworkManager.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
        }

        private void NetworkManager_OnClientDisconnectCallback(ulong client)
        {
            clientStatusMap.Remove(client);
        }

        #endregion
        #region ClientStatus
        [ServerRpc]
        private void SetStatusServerRpc(ulong client, ClientStatus status)
        {
            ClientStatus p_Status = CollectiveStatus;
            clientStatusMap[client] = status;
            SetStatusClientRpc(client, status);
            ClientStatus c_Status = CollectiveStatus;

            Debug.Log(p_Status + " " + c_Status);
            if (p_Status == c_Status)
                return;

            switch (c_Status)
            {
                case ClientStatus.Waiting:
                    currentState.CollectiveActive();
                    break;
                case ClientStatus.Finished:
                    ChangeStateClientRpc((NetworkState.State)(((int)currentState.Type + 1) % Enum.GetValues(typeof(NetworkState.State)).Length));
                    break;
            }
        }
        [ClientRpc]
        private void SetStatusClientRpc(ulong client, ClientStatus status)
        {
            clientStatusMap[client] = status;
        }
        public ClientStatus Status {
            get => clientStatusMap[OwnerClientId];
            set
            {
                SetStatusServerRpc(OwnerClientId, value);
            }
        }
        public ClientStatus CollectiveStatus
        {
            get
            {
                ClientStatus lowestStatus = ClientStatus.Finished;
                foreach (var kvp in clientStatusMap)
                    if (kvp.Value < lowestStatus)
                        lowestStatus = kvp.Value;
                return lowestStatus;
            }
        }
        private Dictionary<ulong, ClientStatus> clientStatusMap;
        #endregion
        #region StateMachine
        [SerializeField] private NetworkState[] states;
        private Dictionary<NetworkState.State, NetworkState> stateMap;
        public Dictionary<NetworkState.State, object> stateData;
        private NetworkState currentState;

        [ClientRpc]
        public void ChangeStateClientRpc(NetworkState.State stateType)
        {
            ChangeState(stateType);
        }
        [ClientRpc]
        public void ActivateStateClientRpc()
        {
            currentState.CollectiveActive();
        }
        private void ChangeState(NetworkState.State stateType)
        {
            Debug.Log("ChangeState " + stateType);
            if (currentState != null)
            {
                if (stateType == currentState.Type)
                    return;
                stateData[currentState.Type] = currentState.GetData;
                currentState.Exit();
            }
            currentState = Instantiate(stateMap[stateType].gameObject).GetComponent<NetworkState>();
            currentState.GetComponent<NetworkObject>().Spawn();
            Status = ClientStatus.Waiting;
        }
        #endregion
    }
}