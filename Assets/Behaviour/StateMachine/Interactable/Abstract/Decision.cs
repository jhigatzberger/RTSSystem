using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(IStateMachine stateMachine);
    }
}
