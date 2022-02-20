using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    public class StartAudioClipAction : Action
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private bool loopUntilExit;
        [SerializeField] private bool playDelayed;
        [SerializeField] private float delay;

        public override void Enter(IStateMachine stateMachine)
        {
            AudioSource source = stateMachine.Entity.MonoBehaviour.GetComponent<AudioSource>();
            Random.InitState((int)LockStep.count);
            int index = Random.Range(0, clips.Length);
            source.clip = clips[index];
            source.loop = loopUntilExit;
            if (source.clip == null)
                return;
            if (playDelayed)
                source.PlayDelayed(delay);
            else
                source.Play();
        }

        public override void Exit(IStateMachine stateMachine)
        {
            AudioSource source = stateMachine.Entity.MonoBehaviour.GetComponent<AudioSource>();
            if (loopUntilExit)
                source.loop = false;
        }
    }
}