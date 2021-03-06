using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    [CreateAssetMenu(fileName = "State", menuName = "RTS/Behaviour/State")]
    public class State : ScriptableObject
    {
        public StateMachineAction[] actions;
        public Transition[] transitions;
        public void Enter(IStateMachine entity)
        {
            foreach (StateMachineAction action in actions)
                action.Enter(entity);
        }
        public void Exit(IStateMachine entity)
        {
            foreach (StateMachineAction action in actions)
                action.Exit(entity);
        }
        public void CheckTransitions(IStateMachine entity)
        {
            foreach(Transition transition in transitions)
            {
                if (transition.decision.Decide(entity))
                {
                    if (transition.trueState != this)
                    {
                        entity.ChangeState(transition.trueState);
                        return;
                    }
                }
                else
                {
                    if (transition.falseState != this)
                    {
                        entity.ChangeState(transition.falseState);
                        return;
                    }
                }
            }
        }
    }
}
