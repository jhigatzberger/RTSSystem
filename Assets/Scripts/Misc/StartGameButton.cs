using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartGameButton : MonoBehaviour
{
    public void OnClick()
    {
        GameStart.StartGame();
        gameObject.SetActive(false);
    }
}
