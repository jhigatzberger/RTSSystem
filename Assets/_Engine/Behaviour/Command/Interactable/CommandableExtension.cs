using System.Collections.Generic;
using System.Linq;
using System;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandableExtension : BaseInteractableExtension<CommandableProperties>, ICommandable
    {

        Queue<CompiledCommand> commandQueue = new Queue<CompiledCommand>();
        public CompiledCommand? Current { get; set; }
        public event Action OnCommandClear;
        public CommandProperties[] CommandCompetence => Properties.commandCompetence;
        public CommandableExtension(IExtendableInteractable entity, CommandableProperties properties) : base(entity, properties){}

        public void Enqueue(CompiledCommand command)
        {
            if (!CommandCompetence.Any(c => CommandData.Instance.CommandToId[c] == command.commandID))
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
            CommandData.Instance.IdToCommand[Current.Value.commandID].Execute(this, Current.Value);
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