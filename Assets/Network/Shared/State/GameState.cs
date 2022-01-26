using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    //https://docs.unity3d.com/Manual/UNetManager.html
    public class GameState : NetworkState
    {
        [SerializeField] private string gameSceneName;
        [SerializeField] private GameObject serverPrefab;
        [SerializeField] private GameObject clientPrefab;
        public override NetworkStateType Type => NetworkStateType.GameState;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            NetworkManager.SceneManager.OnLoadComplete += SceneManager_OnLoadComplete;
            NetworkManager.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);            
        }
        private void SceneManager_OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            if(gameSceneName.Equals(sceneName))
                LoadGame();
        }
        private void LoadGame()
        {
            if (IsServer)
                Instantiate(serverPrefab, transform);
            if (IsClient)
                Instantiate(clientPrefab, transform);
        }
        public void EndGame()
        {
            foreach (Transform t in transform)
                Destroy(t);
        }
    }
}