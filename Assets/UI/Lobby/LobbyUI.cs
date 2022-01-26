using JHiga.RTSEngine;
using JHiga.RTSEngine.Network;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    public void ClickStart()
    {
        NetworkGameManager.Instance.ChangeState(NetworkState.NetworkStateType.GameState); // ONLY ON SERVER!
    }
}
