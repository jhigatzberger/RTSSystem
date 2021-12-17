using System;
using UnityEngine;

namespace RTS.Entity.AI
{
    public abstract class Command : ScriptableObject
    {
        public int id;
        public State state;
        public abstract bool Applicable(AIEntity entity);
        public abstract CommandData Build(AIEntity entity);
        public void Apply(AIEntity entity)
        {
            entity.ChangeState(state);
        }
    }

    public struct CommandData : IEquatable<CommandData>
    {
        public int commandID;
        public Vector3 position;
        public int targetID;

        public bool Equals(CommandData other)
        {
            return other.commandID == commandID && other.position == position && other.targetID == targetID;
        }
    }

}