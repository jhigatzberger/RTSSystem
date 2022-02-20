using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.StateMachine;

namespace JHiga.RTSEngine.Gathering
{
    public class DeliverAction : Action
    {
        public override void Enter(IStateMachine stateMachine)
        {
            IExtendableEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetExtension<IMovable>();
            movable.Clear();
            movable.Enqueue(new Target { entity = entity.GetExtension<IGatherer>().Dropoff.Entity });
        }

        public override void Exit(IStateMachine stateMachine)
        {
            IExtendableEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetExtension<IMovable>();
            movable.Stop();
        }
    }
}