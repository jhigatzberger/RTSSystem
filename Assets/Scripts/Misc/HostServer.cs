using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class HostServer : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        //NetworkSceneManager !
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        NetworkManager.Singleton.StartHost();
    }
}
