using System.Linq;

namespace JHiga.RTSEngine.CommandPattern
{
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
            clearQueueOnEnqeue = skinnedCommandReferences.clearQueueOnEnqueue;
            target = new Target(skinnedCommandReferences.target);
        }
        public ResolvedCommandReferences(IExtendableEntity[] entities, Target target, bool clearQueueOnEnqeue)
        {
            this.entities = entities;
            this.target = target;
            this.clearQueueOnEnqeue = clearQueueOnEnqeue;
        }
        public SkinnedCommandReferences Skin => new SkinnedCommandReferences {
            clearQueueOnEnqueue = clearQueueOnEnqeue,
            target = target.Skin,
            entities = entities.Select(e => e.UniqueID.uniqueId).ToArray()
        };        
    }
}