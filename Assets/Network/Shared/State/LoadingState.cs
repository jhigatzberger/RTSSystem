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
            PlayerContext.players = new PlayerProperties[playerData.Count + 1];
            PlayerContext.players[0] = PlayerProperties.GenerateGaia(RTSWorldData.Instance);
            for (int i = 0; i < playerData.Count; i++)
                PlayerContext.players[playerData[i].PlayerId] = new PlayerProperties(playerData[i].Skin);
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
