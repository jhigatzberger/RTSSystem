using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class LockStepServer : MonoBehaviour
    {
        #region Initialization
        private bool started;
        private float startTime = 0;
        [SerializeField] private float stepSize = 1f;
        public static LockStepServer Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            NetworkGameManager.Instance.currentState.OnValueChanged += OnGameStateChange;
        }

        void OnGameStateChange(NetworkState.State oldState, NetworkState.State newState)
        {
            if (newState == NetworkState.State.Game)
            {
                started = true;
                startTime = Time.time;
            }
        }

        private void OnDestroy()
        {
            NetworkGameManager.Instance.currentState.OnValueChanged -= OnGameStateChange;
        }

        #endregion
        private ulong count;
        private float time;
        private void Update()
        {
            if (!started)
                return;
            if (Time.time - startTime - time > stepSize)
            {
                time = Time.time - startTime;
                LockStepNetwork.Instance.StepClientRPC(time, count++);
            }
        }
    }
}