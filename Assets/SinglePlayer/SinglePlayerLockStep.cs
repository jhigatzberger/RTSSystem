using UnityEngine;

namespace JHiga.RTSEngine.SinglePlayer
{
    public class SinglePlayerLockStep : MonoBehaviour
    {
        [SerializeField] private float stepSize = 1f;
        private ulong count;
        private float time;
        private void Update()
        {
            if (Time.time - time > stepSize)
            {
                time = Time.time;
                LockStep.Step(time, count++);
            }
        }
    }
}