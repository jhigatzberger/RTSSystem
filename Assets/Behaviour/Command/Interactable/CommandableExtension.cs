using System.Collections.Generic;
using System.Linq;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandableExtension : BaseInteractableExtension<CommandableProperties>, ICommandable
    {
        Queue<ResolvedCommand> commandQueue = new Queue<ResolvedCommand>();
        public ResolvedCommand? Current { get; set; }
        public CommandProperties[] CommandCompetence => Properties.commandCompetence;
        public CommandableExtension(IExtendableEntity entity, CommandableProperties properties) : base(entity, properties){}
        public void Enqueue(ResolvedCommand command)
        {
            if (!CommandCompetence.Any(c => c == command.properties))
                return;
            commandQueue.Enqueue(command);
            if (Current == null)
                ExecuteFirstCommand();
        }
        public void ExecuteFirstCommand()
        {
            if (commandQueue.Count == 0)
                return;
            Current = commandQueue.Dequeue();
            if (Properties.onCommandActions != null && Current.Value.references.entities[0].UID.Equals(Entity.UID) && Entity.IsActivePlayer)
            {
                foreach (BaseAction action in Properties.onCommandActions)
                    action.Run(Entity);
            }
            Current.Value.properties.Execute(this, Current.Value.references);
        }
        public override void Clear()
        {
            Current = null;
            commandQueue.Clear();
        }
        public void Finish()
        {
            if (Current.HasValue && Properties.onCommandActions != null && Current.Value.references.entities[0] == Entity && Entity.IsActivePlayer)
            {
                foreach (BaseAction action in Properties.onCommandActions)
                    action.Stop(Entity);
            }
            Current = null;
            ExecuteFirstCommand();
        }
        public CommandProperties FirstApplicableDynamicallyBuildableCommand
        {
            get
            {
                foreach (CommandProperties command in CommandCompetence)
                    if (command.dynamicallyBuildable && command.IsApplicable(this))
                        return command;
                return null;
            }
        }
    }
}