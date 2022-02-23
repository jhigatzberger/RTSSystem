using JHiga.RTSEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerLobby : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject[] previews;

    private int faction;

    private void Awake()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (FactionProperties faction in RTSWorldData.Instance.playableFactions)
            options.Add(new TMP_Dropdown.OptionData(faction.name, faction.icon));
        dropdown.options = options;
        previews[faction].SetActive(true);
    }

    public void OnValueChanged(int value)
    {
        previews[faction].SetActive(false);
        previews[value].SetActive(true);
        faction = value;
    }

    public void Play()
    {
        PlayerContext.players = new PlayerData[2];
        PlayerContext.players[0] = PlayerData.GenerateGaia();
        PlayerContext.PlayerId = 1;
        SkinnedPlayer myPlayer = new SkinnedPlayer
        {
            id = 1,
            faction = (short)RTSWorldData.Instance.playableFactions[faction].id,
            team = 1
        };
        PlayerContext.players[1] = new PlayerData(myPlayer);
        SceneManager.LoadScene("SinglePlayerGame");
    }
}
