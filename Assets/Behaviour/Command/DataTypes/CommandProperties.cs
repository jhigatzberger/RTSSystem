using System.Linq;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{ 
    public abstract class CommandProperties : ScriptableObject
    {
        public Sprite icon;
        public bool dynamicallyBuildable;
        public bool requireContext;
        public abstract bool Applicable(ICommandable entity);
        public abstract Target PackTarget(ICommandable commandable);
        public abstract void Execute(ICommandable commandable, Target target);
        public virtual ResolvedCommand Build(ICommandable reference, IExtendableEntity[] selection, bool clearQueueOnEnqeue)
        {
            return new ResolvedCommand(this, new ResolvedCommandReferences(selection, PackTarget(reference), clearQueueOnEnqeue));
        }
    }
    public struct ResolvedCommand
    {
        public CommandProperties properties;
        public ResolvedCommandReferences references;
        public ResolvedCommand(SkinnedCommand skinnedCommand)
        {
            properties = CommandData.Instance.commands[skinnedCommand.commandId];
            references = new ResolvedCommandReferences(skinnedCommand.references);
        }
        public ResolvedCommand(CommandProperties properties, ResolvedCommandReferences references)
        {
            this.properties = properties;
            this.references = references;
        }
        public SkinnedCommand Skin => new SkinnedCommand
        {
            commandId = CommandData.Instance.CommandToId[properties],
            references = references.Skin
        };
        public void Enqueue()
        {
            foreach(IExtendableEntity entity in references.entities)
            {
                if (entity.TryGetExtension(out ICommandable commandable))
                {
                    if (references.clearQueueOnEnqeue)
                        commandable.Clear();
                    commandable.Enqueue(new SingleResolvedCommand
                    {
                        properties = properties,
                        target = references.target
                    });
                }
            }
        }
    }
    public struct SingleResolvedCommand
    {
        public CommandProperties properties;
        public Target target;
    }
    public struct ResolvedCommandReferences
    {
        public IExtendableEntity[] entities;
        public bool clearQueueOnEnqeue;
        public Target target;
        public ResolvedCommandReferences(SkinnedCommandReferences skinnedCommandReferences)
        {
            entities = new IExtendableEntity[skinnedCommandReferences.entities.Length];
            for(int i = 0; i< entities.Length; i++)
            {
                IExtendableEntity entity = GameEntity.Get(new UID(skinnedCommandReferences.entities[i]));
                entities[i] = entity;
            }
            clearQueueOnEnqeue = skinnedCommandReferences.clearQueueOnEnqeue;
            target = new Target(skinnedCommandReferences.target);
        }
        public ResolvedCommandReferences(IExtendableEntity[] entities, Target target, bool clearQueueOnEnqeue)
        {
            this.entities = entities;
            this.target = target;
            this.clearQueueOnEnqeue = clearQueueOnEnqeue;
        }
        public SkinnedCommandReferences Skin => new SkinnedCommandReferences {
            clearQueueOnEnqeue = clearQueueOnEnqeue,
            target = target.Skin,
            entities = entities.Select(e => e.UniqueID.uniqueId).ToArray()
        };        
    }
    public struct SkinnedCommand
    {
        public ushort commandId;
        public SkinnedCommandReferences references;
    }
    public struct SkinnedCommandReferences
    {
        public int[] entities;
        public bool clearQueueOnEnqeue;
        public SkinnedTarget target;
    }
}