using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    //https://docs.unity3d.com/Manual/UNetManager.html
    public class GameState : NetworkState
    {
        public GameObject serverPrefab;
        public GameObject clientPrefab;
        public override State Type => State.Game;

        private void Start()
        {
            if (IsServer && serverPrefab != null)
                Instantiate(serverPrefab, transform);
            if (IsClient && clientPrefab != null)
                Instantiate(clientPrefab, transform);
        }
    }

}