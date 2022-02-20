using JHiga.RTSEngine.StateMachine;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public class SetGatheringAnimation : Action
    {
        [SerializeField] private string parameterName;
        [SerializeField] private int[] resourceTypeAnimations;
        public override void Enter(IStateMachine stateMachine)
        {
            stateMachine.Entity.MonoBehaviour.GetComponent<Animator>().SetInteger(parameterName, resourceTypeAnimations[stateMachine.Entity.GetExtension<IGatherer>().Target.ResourceType]);
        }
        public override void Exit(IStateMachine stateMachine)
        {
        }
    }
}