using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class test : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("send", 3);
    }


    public void send()
    {
        SendServerRpc(new int[] { 1, 2, 3 });
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendServerRpc(int[] test)
    {
        Debug.Log("sending: " + test.Length);
        ReceiveClientRpc(test);
    }

    [ClientRpc]
    public void ReceiveClientRpc(int[] test)
    {
        Debug.Log("receiving: " + test.Length);
    }

}
