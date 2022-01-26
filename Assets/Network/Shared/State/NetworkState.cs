using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public abstract class NetworkState : NetworkBehaviour
    {
        public enum NetworkStateType
        {
            LobbyState,
            GameState,
            PostGameState
        }
        public abstract NetworkStateType Type { get; }
    }
}
