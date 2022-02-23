using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;

public class SetIP : MonoBehaviour
{
    public void OnValueChanged(string s)
    {
        NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = s;
    }
}
