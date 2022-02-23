using System;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public abstract class CommandProperties : ScriptableObject
    {
        public Sprite icon;
        public virtual string Description { get; }
        public GameObject forcedPreview;
        public bool dynamicallyBuildable;
        public bool requireContext;
        public abstract bool IsApplicable(ICommandable entity, bool forced = false);
        public abstract Target PackTarget(ICommandable commandable);
        public abstract void Execute(ICommandable commandable, ResolvedCommandReferences references);
        public virtual void Request(ICommandable reference, IExtendableEntity[] selection, bool clearQueueOnEnqeue, Action<SkinnedCommand> callback)
        {
            callback(Build(reference, selection, clearQueueOnEnqeue).Skin);
        }
        public virtual ResolvedCommand Build(ICommandable reference, IExtendableEntity[] selection, bool clearQueueOnEnqeue)
        {
            return new ResolvedCommand(this, new ResolvedCommandReferences(selection, PackTarget(reference), clearQueueOnEnqeue));
        }
    }
}