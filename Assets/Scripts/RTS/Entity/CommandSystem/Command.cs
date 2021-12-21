using System;
using UnityEngine;

namespace RTS.Entity
{
    public abstract class Command : ScriptableObject
    {
        public int id;
        public abstract bool Applicable(ICommandable entity);
        public abstract CommandData Build(ICommandable entity);
        public abstract void Execute(ICommandable commandable, CommandData data);
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