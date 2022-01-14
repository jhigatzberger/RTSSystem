using System.Collections.Generic;
using System.Linq;
using System;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandableExtension : BaseInteractableExtension<CommandableProperties>, ICommandable
    {
        Queue<SingleResolvedCommand> commandQueue = new Queue<SingleResolvedCommand>();
        public SingleResolvedCommand? Current { get; set; }
        public event Action OnCommandClear;
        public CommandProperties[] CommandCompetence => Properties.commandCompetence;
        public CommandableExtension(IExtendable entity, CommandableProperties properties) : base(entity, properties){}

        public void Enqueue(SingleResolvedCommand command)
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
            Current.Value.properties.Execute(this, Current.Value.target);
        }
        public override void Clear()
        {
            OnCommandClear?.Invoke();
            Current = null;
            commandQueue.Clear();
        }
        public void Finish()
        {
            Current = null;
            ExecuteFirstCommand();
        }

        public CommandProperties FirstApplicableDynamicallyBuildableCommand
        {
            get
            {
                foreach (CommandProperties command in CommandCompetence)
                    if (command.dynamicallyBuildable && command.Applicable(this))
                        return command;
                return null;
            }
        }
    }
}