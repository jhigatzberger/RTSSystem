using System;
using UnityEngine;

namespace RTS.Entity.AI
{
    public abstract class Command : ScriptableObject
    {
        public State state;
        public abstract bool Applicable(AIEntity entity);
        public abstract CommandData Build(AIEntity entity);
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