using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class JoinServer : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        NetworkManager.Singleton.StartClient();
    }
}
