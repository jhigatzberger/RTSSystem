using UnityEngine;

namespace RTS.AI
{
    [CreateAssetMenu(fileName = "MoveCommand", menuName = "RTS/AI/MoveCommand")]
    public class Command : ScriptableObject
    {
        public State state;

        public void Apply(AIEntity entity)
        {
            entity.ChangeState(state);
        }
    }

    public struct CommandData
    {
        public Command command;
        public Vector3? position;
        public BaseEntity target;
    }

}