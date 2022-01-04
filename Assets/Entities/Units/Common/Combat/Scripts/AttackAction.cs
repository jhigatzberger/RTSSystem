using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "RTS/AI/Actions/AttackAction")]
public class AttackAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        stateMachine.Extendable.GetScriptableComponent<IAttacker>().Attack();
    }

    public override void Exit(IStateMachine entity)
    {
    }
        
}
