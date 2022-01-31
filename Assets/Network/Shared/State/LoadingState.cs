using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    public class LoadingState : NetworkState
    {
        public override State Type => State.Loading;

        public override object StateData => null;

        public override void OnCollectiveActive()
        {
            string scene = ((LobbyData)NetworkGameManager.Instance.stateData[State.Lobby]).mapName;
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            operation.completed += Operation_completed;
        }

        private void Operation_completed(AsyncOperation asyncOperation)
        {
            Finish();
        }

        public override void OnExit()
        {
        }

    }
}
