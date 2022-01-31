using JHiga.RTSEngine;
using JHiga.RTSEngine.Network;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private Dictionary<ulong, GameObject> players;

    public Dropdown factionDropdown;

    private void Start()
    {
        factionDropdown.options = RTSWorldData.Instance.playableFactions.Select(f => new OptionData
        {
            text = f.name
        }).ToList();
        factionDropdown.onValueChanged.AddListener(ChangeEvent);
        LobbyState.OnData += Instance_OnData;
    }

    private void Instance_OnData(LobbyData data)
    {
        Debug.Log(data.players.First(p=>p.clientId == NetworkManager.Singleton.LocalClientId));
    }

    private void ChangeEvent(int value)
    {
        LobbyState.Instance.ChooseFaction(value);
    }

    public void ClickReady()
    {
        NetworkGameManager.Instance.Status = ClientStatus.Finished;
    }
}
