using UnityEngine;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "State", menuName = "RTS/AI/State")]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;
        public void Enter(IStateMachine entity)
        {
            foreach (Action action in actions)
                action.Enter(entity);
        }
        public void Exit(IStateMachine entity)
        {
            foreach (Action action in actions)
                action.Exit(entity);
        }
        public void CheckTransitions(IStateMachine entity)
        {
            foreach(Transition transition in transitions)
            {
                if (transition.decision.Decide(entity))
                    entity.ChangeState(transition.trueState);
                else
                    entity.ChangeState(transition.falseState);
            }
        }
    }
}
