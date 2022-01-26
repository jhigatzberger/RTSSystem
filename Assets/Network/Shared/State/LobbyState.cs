using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class LobbyState : NetworkState
    {
        public override NetworkStateType Type => NetworkStateType.LobbyState;

        public override void OnNetworkSpawn()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}