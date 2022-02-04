using JHiga.RTSEngine;
using JHiga.RTSEngine.Network;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] private Text playerName;
    [SerializeField] private Image playerColor;
    [SerializeField] private Dropdown factionDropdown;
    private int _playerFaction = 0;
    private int PlayerFaction
    {
        set
        {
            if(value != _playerFaction)
            {
                _playerFaction = value;
                if(IsActivePlayer)
                    LobbyState.Instance.ChooseFaction((short)value);
                else
                    factionDropdown.value = value;
            }
        }
    }

    private bool _isActivePlayer;
    public bool IsActivePlayer {
        get => _isActivePlayer;
        set
        {
            _isActivePlayer = value;
            factionDropdown.interactable = value;
        }
    }

    private void Start()
    {
        factionDropdown.options = RTSWorldData.Instance.playableFactions.Select(f => new OptionData
        {
            text = f.name
        }).ToList();
        factionDropdown.onValueChanged.AddListener(ChangeEvent);
    }

    private void ChangeEvent(int value)
    {
        PlayerFaction = value;
    }

    public void Display(PlayerState playerData)
    {
        PlayerFaction = playerData.factionId;
        playerName.text = "Player " + playerData.PlayerId;
        playerColor.color = RTSWorldData.Instance.playerColors[playerData.PlayerId];
    }

}
