using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using JHiga.RTSEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public class CommandableExtension : Extension, ICommandable
    {
        public Command[] CommandCompetence => commandCompetence;
        Queue<CommandData> commandQueue = new Queue<CommandData>();
        public CommandData? Current { get; set; }
        public event Action OnCommandClear;
        private Command[] commandCompetence;
        public CommandableExtension(IExtendable entity, Command[] commandCompetence) : base(entity)
        {
            this.commandCompetence = commandCompetence;
        }

        public void Enqueue(CommandData command)
        {
            if (!commandCompetence.Contains(CommandManager.Commands[command.commandID]))
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
            CommandManager.Execute(this, Current.Value);
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

        public Command FirstApplicableDynamicallyBuildableCommand
        {
            get
            {
                foreach (Command command in commandCompetence)
                    if (command.dynamicallyBuildable && command.Applicable(this))
                        return command;
                return null;
            }
        }
    }
}