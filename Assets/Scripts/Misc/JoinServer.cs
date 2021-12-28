using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class JoinServer : MonoBehaviour
{
    public GameObject hostButton;
    public GameObject startButton;


    public void OnClick()
    { 
        NetworkManager.Singleton.StartClient();
        gameObject.SetActive(false);
        hostButton.SetActive(false);
        startButton.SetActive(false);
    }
}
