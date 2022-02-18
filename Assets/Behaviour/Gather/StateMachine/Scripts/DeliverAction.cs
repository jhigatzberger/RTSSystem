using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.StateMachine;

namespace JHiga.RTSEngine.Gather
{
    public class DeliverAction : Action
    {
        public override void Enter(IStateMachine stateMachine)
        {
            IExtendableEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetExtension<IMovable>();
            movable.Clear();
            movable.Enqueue(new Target { position = entity.GetExtension<IGatherer>().Dropoff.Position });
        }

        public override void Exit(IStateMachine stateMachine)
        {
            IExtendableEntity entity = stateMachine.Entity;
            IMovable movable = entity.GetExtension<IMovable>();
            movable.Stop();
        }
    }
}