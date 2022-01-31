using System;
using System.Linq;
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
                IReadOnlyList<ulong> clients = NetworkManager.Singleton.ConnectedClientsIds;
                LobbyData data = new LobbyData
                {
                    mapName = RTSWorldData.Instance.mapNames[0],
                    players = new PlayerData[clients.Count]
                };
                for (int i = 0; i < clients.Count; i++)
                {
                    data.players[i] = (new PlayerData
                    {
                        name = "Player" + clients[i].ToString(),
                        clientId = clients[i],
                        factionId = 0
                    });
                }                    
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

        public override object StateData => _data;

        public static event Action<LobbyData> OnData;

        public override void OnCollectiveActive()
        {
            Instance = this;
            if (IsServer)
                Data = DefaultData;
            else
                RequestSynchronizeLobbyDataServerRpc(OwnerClientId);
            PlayerContext.PlayerId = (int)NetworkManager.LocalClientId + 1;
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
            Data.players.First(p => p.clientId == client).factionId = faction;
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
    
    public class LobbyData : INetworkSerializable
    {
        public string mapName;
        public PlayerData[] players;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        { 
            int length = 0;

            if (!serializer.IsReader)
            {
                length = players.Length;
            }

            serializer.SerializeValue(ref length);
            serializer.SerializeValue(ref mapName);

            if (serializer.IsReader)
            {
                players = new PlayerData[length];
            }

            for (int n = 0; n < length; ++n)
            {
                serializer.SerializeNetworkSerializable(ref players[n]);
            }
        }
    }

    public class PlayerData : INetworkSerializable
    {
        public ulong clientId;
        public string name;
        public short factionId;
        public int team;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientId);
            serializer.SerializeValue(ref name);
            serializer.SerializeValue(ref factionId);
            serializer.SerializeValue(ref team);
        }
    }
}