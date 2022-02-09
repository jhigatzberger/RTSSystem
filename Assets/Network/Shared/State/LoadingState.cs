using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    public class LoadingState : NetworkState
    {
        public GameObject serverPrefab;
        public GameObject clientPrefab;
        public override State Type => State.Loading;

        private void Start()
        {
            var playerData = NetworkGameManager.Instance.playerData;
            PlayerContext.PlayerId = (int)NetworkManager.LocalClientId + 1;
            PlayerContext.players = new PlayerProperties[playerData.Count + 1];
            PlayerContext.players[0] = PlayerProperties.GenerateGaia(RTSWorldData.Instance);
            for (int i = 0; i < playerData.Count; i++)
                PlayerContext.players[playerData[i].PlayerId] = new PlayerProperties(playerData[i].Skin);
            Invoke("LoadScene", 5);
        }

        private void LoadScene()
        {
            string scene = RTSWorldData.Instance.mapNames[NetworkGameManager.Instance.sessionData.Value.mapIndex];
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            operation.completed += OnLoad;
        }
        private void OnLoad(AsyncOperation asyncOperation)
        {
           
            if (IsServer && serverPrefab != null)
                Instantiate(serverPrefab);
            if (IsClient && clientPrefab != null)
                Instantiate(clientPrefab);
            Debug.Log(SceneManager.GetActiveScene().name); Invoke("Finish", 5);
        }
    }
}
