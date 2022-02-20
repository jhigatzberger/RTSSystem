using JHiga.RTSEngine.StateMachine;

namespace JHiga.RTSEngine.Gathering
{
    public class GatherAction : Action
    {
        public override void Enter(IStateMachine stateMachine)
        {
            stateMachine.Entity.GetExtension<IGatherer>().Gather();
        }

        public override void Exit(IStateMachine stateMachine)
        {
        }
    }
}
