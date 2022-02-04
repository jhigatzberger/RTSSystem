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
            var playerData = NetworkGameManager.Instance.playerData;
            PlayerContext.PlayerId = (int)NetworkManager.LocalClientId + 1;
            PlayerContext.players = new PlayerProperties[playerData.Count];
            for (int i = 0; i < PlayerContext.players.Length; i++)
                PlayerContext.players[i] = new PlayerProperties(playerData[i].Skin);
            LoadScene();
        }

        private void LoadScene()
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
