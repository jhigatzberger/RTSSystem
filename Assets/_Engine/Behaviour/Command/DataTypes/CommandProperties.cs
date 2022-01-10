using System;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{ 
    public abstract class CommandProperties : ScriptableObject
    {
        public Sprite icon;
        public bool dynamicallyBuildable;
        public bool requireContext;
        public abstract bool Applicable(ICommandable entity);
        public abstract CompiledCommand Compile();
        public abstract void Execute(ICommandable commandable, CompiledCommand data);
    }
}