using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "StartAudioClipBaseAction", menuName = "RTS/Behaviour/Actions/StartAudioClipBaseAction")]
    public class StartAudioClipBaseAction : BaseAction
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private bool loopUntilExit;
        [SerializeField] private bool playDelayed;
        [SerializeField] private float delay;

        public override void Run(IExtendableEntity entity)
        {
            AudioSource source = entity.MonoBehaviour.GetComponent<AudioSource>();
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

        public override void Stop(IExtendableEntity entity)
        {
            AudioSource source = entity.MonoBehaviour.GetComponent<AudioSource>();
            if (loopUntilExit)
                source.loop = false;
        }
    }
}