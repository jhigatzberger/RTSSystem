using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    public class NetworkGameManager : NetworkBehaviour
    {
        public enum GameState // actually reasonable handle this shit
        {
            Pre = 1,
            Game = 2,
            Post = 3
        }

        #region Singleton
        public static NetworkGameManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null || !IsOwner)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }
        #endregion

        [SerializeField] private GameObject serverObject;
        [SerializeField] private GameObject clientObject;
        [SerializeField] private GameObject sharedObject;

        public void StartGame()
        {
            PlayerContext.PlayerId = (int)NetworkManager.LocalClientId+1;
            SceneManager.LoadScene(2);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 2)
                LoadGame();
            else if (arg0.buildIndex == 3)
                EndGame();
        }

        private void LoadGame()
        {

            if (IsServer)
                Instantiate(serverObject, transform);
            if (IsClient)
                Instantiate(clientObject, transform);
            NetworkObject net = Instantiate(sharedObject, transform).GetComponent<NetworkObject>();
            net.Spawn();

        }

        public void EndGame()
        {
            foreach (Transform t in transform)
                Destroy(t);
        }
    }
}