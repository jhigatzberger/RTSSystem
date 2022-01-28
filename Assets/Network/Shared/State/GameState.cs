using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    //https://docs.unity3d.com/Manual/UNetManager.html
    public class GameState : NetworkState
    {
        public override State Type => State.Game;
        public NetworkGameData gameData;
        public override object GetData => gameData;

        public override void OnCollectiveActive()
        {
            throw new System.NotImplementedException();
        }


        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }

    public struct NetworkGameData
    {

    }
}