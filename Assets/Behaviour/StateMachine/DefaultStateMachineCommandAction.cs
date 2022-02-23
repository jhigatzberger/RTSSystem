using JHiga.RTSEngine.StateMachine;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    [CreateAssetMenu(fileName = "DefaultStateMachineCommandAction", menuName = "RTS/Behaviour/Actions/DefaultStateMachineCommandAction")]
    public class DefaultStateMachineCommandAction : CommandAction
    {
        [SerializeField] private State state;
        public override void Execute(ICommandable commandable, ResolvedCommandReferences references)
        {
            commandable.Entity.GetExtension<ITargeter>().Target = references.target;
            commandable.Entity.GetExtension<IStateMachine>().ChangeState(state);
        }
    }
}