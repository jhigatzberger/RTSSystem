using UnityEngine;
using Unity.Netcode;

public class HostServer : MonoBehaviour
{
    [SerializeField] private GameObject networkGameManager;
    public void OnClick()
    {
        NetworkManager.Singleton.StartHost();
        Instantiate(networkGameManager).GetComponent<NetworkObject>().Spawn();
    }

}
