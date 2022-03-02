using JHiga.RTSEngine;
using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToGameEntitytWithTag", menuName = "RTS/Behaviour/Actions/MoveToGameEntitytWithTag")]
public class MoveToGameEntitytWithTag : StateMachineAction
{
    [SerializeField] private string tag;
    private Target target;

    public override void Enter(IStateMachine stateMachine)
    {
        target = new Target { position = GameObject.FindGameObjectWithTag(tag).GetComponent<GameEntity>().ClosestEdgePoint(stateMachine.Entity.Position) };
        Debug.Log(target.ClosestPoint(stateMachine.Entity.Position));
        
        IMovable movable = stateMachine.Entity.GetExtension<IMovable>();
        if (movable.Destination == null)
            movable.Enqueue(target);
    }

    public override void Exit(IStateMachine stateMachine)
    {
    }
}
