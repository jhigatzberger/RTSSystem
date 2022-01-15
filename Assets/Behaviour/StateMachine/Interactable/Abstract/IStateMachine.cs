using JHiga.RTSEngine.CommandPattern;

namespace JHiga.RTSEngine.StateMachine
{
    /// <summary>
    /// Handles the <see cref="State"/> trees of unit behaviours.
    /// Tightly coupled to the <see cref="ICommandable"/>  <see cref="IEntityExtension"/> to allow a seamless notification of <see cref="ICommandable.Finish"/> when a statechain ends.
    /// </summary>
    public interface IStateMachine : IEntityExtension
    {
        /// <summary>
        /// The <see cref="LockStep"/> time stamp of the last time the <see cref="State"/> has been changed.
        /// </summary>
        public float TimeStamp { get; set; }
        
        /// <summary>
        /// Calls the <see cref="State.Exit"/> method of the current <see cref="State"/>.
        /// Calls the <see cref="State.Enter"/> method of the new <see cref="State"/>.
        /// Caches the <see cref="TimeStamp"/>.
        /// When the state reaches a null state <see cref="ICommandable.Finish"/> could be notified.
        /// </summary>
        public void ChangeState(State state);        
        
        /// <summary>
        /// Calls <see cref="ChangeState"/> with the result of the <see cref="Decision"/> of all the <see cref="Transition"/> of the current <see cref="State"/>.
        /// If the current <see cref="State"/> is null change to a default state.
        /// </summary>
        public void UpdateState();
    }
}
