using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    public class LobbyState : NetworkState
    {
        public static LobbyState Instance { get; private set; }

        public override State Type => State.Lobby;

        private void Start()
        {
            Instance = this;
            SceneManager.LoadScene(1);
        }             

        public void ChooseFaction(short faction)
        {
            Debug.Log("choosing faction: " + faction + " " + NetworkManager.LocalClientId);

            ChooseFactionServerRpc(faction, NetworkManager.LocalClientId);
        }

        [ServerRpc(RequireOwnership = false)]
        private void ChooseFactionServerRpc(short faction, ulong clientId)
        {
            var playerData = NetworkGameManager.Instance.playerData;
            for (int i = 0; i < playerData.Count; i++)
            {
                if (playerData[i].clientId == clientId)
                    playerData[i] = new PlayerState
                    {
                        factionId = faction,
                        status = playerData[i].status,
                        clientId = playerData[i].clientId,
                        team = playerData[i].team
                    };
            }

        }
    }    

}