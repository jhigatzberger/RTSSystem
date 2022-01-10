using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class HostServer : MonoBehaviour
{
    public void OnClick()
    {
        NetworkManager.Singleton.StartHost();
        SceneManager.LoadScene(1);
    }

}
