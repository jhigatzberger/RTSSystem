using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Command
{
    public abstract class BaseCommand
    {
        public bool running = false;
        protected ICommandable commandable;
        public BaseCommand(ICommandable commandable)
        {
            this.commandable = commandable;
        }
        public abstract void OnExecute();
        public abstract void OnStop();

        public void Stop()
        {
            if (!running)
                return;
            Debug.Log("stopped " + this);
            running = false;
            OnStop();
        }
        public void Execute()
        {
            if (running)
                return;
            Debug.Log("executing " + this);
            running = true;
            OnExecute();
        }
        public void Finish()
        {
            if (!running)
                return;
            Debug.Log("finished " + this);
            running = false;
            commandable.ExecuteFirst();
        }
    }

}