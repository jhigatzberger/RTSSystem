using JHiga.RTSEngine.StateMachine;

namespace JHiga.RTSEngine.Construction
{
    public class ConstructAction : StateMachineAction
    {
        public override void Enter(IStateMachine stateMachine)
        {
            stateMachine.Entity.GetExtension<IBuilder>().Construct();
        }

        public override void Exit(IStateMachine stateMachine)
        {
        }
    }
}