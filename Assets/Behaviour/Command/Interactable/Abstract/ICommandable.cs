using JHiga.RTSEngine.StateMachine;

namespace JHiga.RTSEngine.CommandPattern
{
    public interface ICommandable : IInteractableExtension
    {
        public SingleResolvedCommand? Current { get; set; }
        /// <summary>
        /// Gets called when a command has been fully performed.
        /// Resets <see cref="Current"/> and calls <see cref="ExecuteFirstCommand"/> to work the command queue down.
        /// E.g.: When <see cref="IStateMachine.ChangeState(State)"/> has reached a null state and the state chain ends.
        /// </summary>
        public void Finish();
        /// <summary>
        /// All <see cref="CommandProperties"/> the <see cref="IExtendable"/> can <see cref="CommandProperties.PackTarget"/>.
        /// It is assumed that the lower the index of the element, the more important the command is. 
        /// E.g.: If you hover over an enemy unit you can perform both the attack and the move command, however only the attack command should be performed.
        /// </summary>
        public CommandProperties[] CommandCompetence { get; }
        public CommandProperties FirstApplicableDynamicallyBuildableCommand { get; }
        public void Enqueue(SingleResolvedCommand command);
        public void ExecuteFirstCommand();
    }

}
