using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class JoinServer : MonoBehaviour
{
    public void OnClick()
    {
        NetworkManager.Singleton.StartClient();
    }
}
