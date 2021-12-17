using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RTS.Entity;

namespace RTS.Entity.AI
{
    public class AIEntity : BaseEntity
    {
        public override int Priority => 10;

        [Tooltip("Watch out for the order! The earlier the command, the more it will be prioritized.")]
        public Command[] commandCompetence;

        Queue<CommandData> commandQueue = new Queue<CommandData>();

        public CommandData? currentCommand;
        private State currentState;

        public delegate void CommandClear();
        public event CommandClear OnCommandClear;

        [SerializeField] private State defaultState;

        public float stateTimeStamp;

        private void Awake()
        {
            AIManager.entities.Add(this);
        }

        public void AIUpdate() // gets server time passed, save server time
        {
            if (currentState != null)
                currentState.CheckTransitions(this);
            else if(defaultState != null)
                ChangeState(defaultState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(currentCommand.HasValue)
                Gizmos.DrawSphere(currentCommand.Value.position, 1);
        }

        public void ChangeState(State state)
        {
            if (state == currentState)
                return;
            if (currentState != null)
                currentState.Exit(this);
            currentState = state;
            if (currentState != null)
                currentState.Enter(this);
            else if (commandQueue.Count > 0)
                ExecuteFirstCommand();
            else
                currentCommand = null;
            stateTimeStamp = Time.time; // take last server time
        }  
        public void Enqueue(CommandData command)
        {
            if (!commandCompetence.Contains(CommandManager.Commands[command.commandID]))
                return;
            commandQueue.Enqueue(command);
            if (currentCommand == null)
                ExecuteFirstCommand();
        }

        public void ExecuteFirstCommand()
        {
            currentCommand = commandQueue.Dequeue();
            if(currentCommand.HasValue)
                CommandManager.Commands[currentCommand.Value.commandID].Apply(this);
        }

        public void ClearCommands()
        {
            currentState = null;
            currentCommand = null;
            commandQueue.Clear();
            OnCommandClear?.Invoke();
        }

        public Command FirstApplicableCommand
        {
            get
            {
                foreach (Command command in commandCompetence)
                    if (command.Applicable(this))
                        return command;
                return null;
            }
        }

        public CommandData? GetCommandFromContext()
        {
            foreach (Command command in commandCompetence)
                if (command.Applicable(this))
                    return command.Build(this);
            return null;
        }

        protected override void OnExitScene()
        {
            base.OnExitScene();
            ClearCommands();
            AIManager.entities.Remove(this);
        }
    }

}
