using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HostServer : MonoBehaviour
{
    public GameObject joinButton;
    public GameObject startButton;


    public void OnClick()
    {
        NetworkManager.Singleton.StartHost();
        gameObject.SetActive(false);
        joinButton.SetActive(false);
        startButton.SetActive(true);
    }
}
