using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.AI
{
    public class AIEntity : BaseEntity
    {
        public override int Priority => 10;

        private AIEntityExtension[] extensions;
        public Command[] commandCompetence;

        Queue<CommandData> commandQueue = new Queue<CommandData>();

        public CommandData? currentCommand;
        private State currentState;

        private void Awake()
        {
            extensions = GetComponents<AIEntityExtension>().OrderBy(e => e.Priority).ToArray();
            commandCompetence = new Command[extensions.Length];
            for (int i = extensions.Length; i-- > 0;)
                commandCompetence[i] = extensions[i].Command;
        }

        private void FixedUpdate()
        {
            if(currentState != null)
                currentState.CheckTransitions(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(currentCommand.HasValue)
                Gizmos.DrawSphere(currentCommand.Value.position.Value, 1);
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
        }  
        public void Enqueue(CommandData command)
        {
            commandQueue.Enqueue(command);
            if (currentCommand == null)
                ExecuteFirstCommand();
        }

        public void ExecuteFirstCommand()
        {
            Debug.Log(commandQueue.Count);
            currentCommand = commandQueue.Dequeue();
            currentCommand?.command.Apply(this);
        }

        public void StopAndClear()
        {
            currentState = null;
            currentCommand = null;
            commandQueue.Clear();
        }

        public CommandData? GetCommandFromContext()
        {
            foreach (AIEntityExtension extension in extensions)
                if (extension.Applicable)
                    return extension.ContextPoll();
            return null;
        }

        protected override void OnExitScene()
        {
            base.OnExitScene();
            StopAndClear();
        }
    }

}
