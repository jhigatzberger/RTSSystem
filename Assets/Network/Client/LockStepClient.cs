using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class LockStepClient : MonoBehaviour
    {
        #region Initialization
        private bool started;
        private float startTime = 0;
        [SerializeField] private float stepSize = 1f;
        public static LockStepClient Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
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
        private void FixedUpdate()
        {
            if (!started)
                return;
            if (Time.time - startTime - time > stepSize)
            {
                time = Time.time - startTime;
                LockStep.Step(time, count++);
            }
        }
    }
}