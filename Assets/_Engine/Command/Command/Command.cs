using System;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public abstract class Command : ScriptableObject
    {
        public int id;
        public Sprite icon;
        public bool dynamicallyBuildable;
        public bool requireContext;
        public abstract bool Applicable(ICommandable entity);
        public abstract CommandData Build();
        public abstract void Execute(ICommandable commandable, CommandData data);
    }

    public struct CommandData
    {
        public int commandID;
        public Vector3 position;
        public int targetID;
    }

}