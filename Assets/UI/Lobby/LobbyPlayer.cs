using JHiga.RTSEngine;
using JHiga.RTSEngine.Network;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] private Dropdown factionDropdown;

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
        LobbyState.Instance.ChooseFaction((short)value);
    }

    public void Display(PlayerState playerData)
    {
        factionDropdown.value = playerData.factionId;
    }

}
