using System;
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
        public ResolvedCommand(CommandProperties properties, bool clearQueueOnEnqeue, IExtendable[] entities, ICommandable referenceCommandable)
        {
            this.properties = properties;
            references = new ResolvedCommandReferences
            {
                clearQueueOnEnqeue = clearQueueOnEnqeue,
                target = properties.PackTarget(referenceCommandable),
                entities = entities
            };
        }
        public SkinnedCommand Skin => new SkinnedCommand
        {
            commandId = CommandData.Instance.CommandToId[properties],
            references = references.Skin
        };
        public void Enqueue()
        {
            foreach(IExtendable entity in references.entities)
            {
                Debug.Log(references.entities.Length + " " + (entity == null) + " " + (references.entities == null));
                if (entity.TryGetScriptableComponent(out ICommandable commandable))
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
        public IExtendable[] entities;
        public bool clearQueueOnEnqeue;
        public Target target;
        public ResolvedCommandReferences(SkinnedCommandReferences skinnedCommandReferences)
        {
            entities = new IExtendable[skinnedCommandReferences.entities.Length];
            for(int i = 0; i< entities.Length; i++)
            {
                IExtendable entity = EntityConstants.FindEntityByUniqueId(new UID(skinnedCommandReferences.entities[i]));
                Debug.Log((entity == null) + " " + skinnedCommandReferences.entities[i]);
                entities[i] = entity;
            }
            clearQueueOnEnqeue = skinnedCommandReferences.clearQueueOnEnqeue;
            target = new Target(skinnedCommandReferences.target);
        }
        public SkinnedCommandReferences Skin => new SkinnedCommandReferences {
            clearQueueOnEnqeue = clearQueueOnEnqeue,
            target = target.Skin,
            entities = entities.Select(e => e.EntityId.uniqueId).ToArray()
        };        
    }
    public struct SkinnedCommand
    {
        public int commandId;
        public SkinnedCommandReferences references;
    }
    public struct SkinnedCommandReferences
    {
        public int[] entities;
        public bool clearQueueOnEnqeue;
        public SkinnedTarget target;
    }
}