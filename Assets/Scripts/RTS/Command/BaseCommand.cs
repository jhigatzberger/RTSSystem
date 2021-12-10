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
            running = false;
            OnStop();
        }
        public void Execute()
        {
            if (running)
                return;
            running = true;
            OnExecute();
        }
        public void Finish()
        {
            if (!running)
                return;
            running = false;
            commandable.ExecuteFirst();
        }
    }

}