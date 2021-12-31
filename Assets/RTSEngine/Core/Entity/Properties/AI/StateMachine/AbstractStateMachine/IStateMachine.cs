namespace RTSEngine.Core.AI
{
    /// <summary>
    /// Handles <see cref="State"/> trees
    /// </summary>
    public interface IStateMachine : IExtension
    {
        /// <summary>
        /// The <see cref="LockStep"/> time stamp of the last time the <see cref="State"/> has been changed.
        /// </summary>
        public float TimeStamp { get; set; }
        
        /// <summary>
        /// Calls the <see cref="State.Exit"/> method of the current <see cref="State"/>.
        /// Calls the <see cref="State.Enter"/> method of the new <see cref="State"/>.
        /// Caches the <see cref="TimeStamp"/>.
        /// Changing to null invokes <see cref="OnStateChainEnd"/>.
        /// </summary>
        public void ChangeState(State state);
        
        /// <summary>
        /// Calls <see cref="ChangeState"/> with null.
        /// </summary>
        public void ResetState();
        
        /// <summary>
        /// Calls <see cref="ChangeState"/> with the result of the <see cref="Decision"/> of all the <see cref="Transition"/> of the current <see cref="State"/>.
        /// If the current <see cref="State"/> is null change to a default state.
        /// </summary>
        public void UpdateState();

        /// <summary>
        /// Invoked when calling <see cref="ChangeState"/> with null.
        /// </summary>
        public event System.Action OnStateChainEnd;
    }
}
