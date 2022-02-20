using JHiga.RTSEngine.StateMachine;

namespace JHiga.RTSEngine.Construction
{
    public class ConstructAction : Action
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