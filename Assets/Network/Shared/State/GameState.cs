using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    //https://docs.unity3d.com/Manual/UNetManager.html
    public class GameState : NetworkState
    {
        public override State Type => State.Game;

        public override void OnDestroy()
        {
            base.OnDestroy();
            Debug.Log("DESTROY GameState");
        }
    }

}