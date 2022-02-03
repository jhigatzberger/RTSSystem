using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    public class LoadingState : NetworkState
    {
        public override State Type => State.Loading;

        private void Start()
        {
            string scene = RTSWorldData.Instance.mapNames[NetworkGameManager.Instance.sessionData.Value.mapIndex];
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            operation.completed += Operation_completed;
        }
        private void Operation_completed(AsyncOperation asyncOperation)
        {
            Finish();
        }
    }
}
