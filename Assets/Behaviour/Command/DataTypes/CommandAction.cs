using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public abstract class CommandAction : ScriptableObject
    {
        public abstract void Execute(ICommandable commandable, ResolvedCommandReferences references);
    }
}