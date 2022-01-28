using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class LobbyState : NetworkState
    {
        public static LobbyState Instance { get; private set; }

        private LobbyData _data;
        private LobbyData DefaultData
        {
            get
            {
                LobbyData data = new LobbyData
                {
                    mapName = RTSWorldData.Instance.mapNames[0],
                    players = new List<PlayerData>()
                };
                foreach (ulong client in NetworkManager.Singleton.ConnectedClientsIds)
                    data.players.Add(new PlayerData
                    {
                        name = "Player" + client.ToString(),
                        clientId = client,
                        factionId = 0
                    });
                return data;
            }
        }
        private LobbyData Data 
        {
            get
            {
                if (_data == null)
                    _data = DefaultData;
                OnData?.Invoke(_data);
                return _data;
            }
            set
            {
                _data = value;
                OnData?.Invoke(value);
            }
        }

        public override State Type => State.Lobby;

        public override object GetData => _data;

        public static event Action<LobbyData> OnData;

        public override void OnCollectiveActive()
        {
            Instance = this;
            if (IsServer)
                Data = DefaultData;
            else
                RequestSynchronizeLobbyDataServerRpc(OwnerClientId);
        }

        public void ChooseFaction(int faction)
        {
            ChooseFactionServerRpc(OwnerClientId, (short)faction);
        }

        [ServerRpc]
        private void ChooseFactionServerRpc(ulong client, short faction)
        {
            ChooseFactionClientRpc(client, faction);
        }

        [ClientRpc]
        private void ChooseFactionClientRpc(ulong client, short faction)
        {
            Data.players.Find(p => p.clientId == client).factionId = faction;
        }

        [ServerRpc]
        public void RequestSynchronizeLobbyDataServerRpc(ulong client)
        {
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { client }
                }
            };
            SynchroniseLobbyDataClientRpc(Data, clientRpcParams);
        }

        [ClientRpc]
        public void SynchroniseLobbyDataClientRpc(LobbyData data, ClientRpcParams clientRpcParams = default)
        {
            Data = data;
        }
    }
    
    public class LobbyData
    {
        public string mapName;
        public List<PlayerData> players;
    }

    public class PlayerData
    {
        public ulong clientId;
        public string name;
        public short factionId;
    }
}