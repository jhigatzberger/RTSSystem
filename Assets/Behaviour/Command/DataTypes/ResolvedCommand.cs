namespace JHiga.RTSEngine.CommandPattern
{
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
                        commandable.Entity.Clear();
                    commandable.Enqueue(this);
                }
            }
        }
    }
}