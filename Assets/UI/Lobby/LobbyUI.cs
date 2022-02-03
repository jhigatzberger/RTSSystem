using JHiga.RTSEngine.Network;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private Dictionary<ulong, LobbyPlayer> players = new Dictionary<ulong, LobbyPlayer>();


    private void Start()
    {
        NetworkGameManager.Instance.playerData.OnListChanged += PlayerData_OnListChanged;
        HandleLobbyPlayerUI();
    }


    private void OnDestroy()
    {
        NetworkGameManager.Instance.playerData.OnListChanged -= PlayerData_OnListChanged;
    }

    void HandleLobbyPlayerUI()
    {
        var playerData = NetworkGameManager.Instance.playerData;
        HashSet<ulong> oldPlayerIds = new HashSet<ulong>(players.Keys);
        foreach (PlayerState p in playerData)
        {
            oldPlayerIds.Remove(p.clientId);
            if (!players.TryGetValue(p.clientId, out LobbyPlayer lobbyPlayer))
            {
                lobbyPlayer = Instantiate(playerPrefab, transform).GetComponent<LobbyPlayer>();
                players[p.clientId] = lobbyPlayer;
                lobbyPlayer.transform.Translate(Vector3.down * p.clientId * 50);
            }
            lobbyPlayer.Display(p);
        }
        foreach (ulong id in oldPlayerIds)
        {
            LobbyPlayer lp = players[id];
            players[id] = null;
            Destroy(lp.gameObject);
        }
    }

    private void PlayerData_OnListChanged(NetworkListEvent<PlayerState> changeEvent)
    {
        HandleLobbyPlayerUI();
    }
    public void ClickReady()
    {
        NetworkGameManager.Instance.Status = PlayerStatus.Ready;
    }
}
