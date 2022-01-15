using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.Movement
{
    public interface IMovable : IEntityExtension
    {        
        /// <summary>
        /// Instantiates a <see cref="Entity.Destination"/> and enqueues it for later <see cref="IMovable.Move"/>, if there is no other <see cref="IMovable.Destination"/> in queue, <see cref="IMovable.Move"/>.
        /// </summary>
        public void Enqueue(Target destination);
        
        /// <summary>
        /// Pops the first Element in the Queue and make it the current <see cref="IMovable.Destination"/>.
        /// </summary>
        public void Move();  
        
        /// <summary>
        /// Sets the <see cref="IMovable.Destination"/> null.
        /// </summary>
        public void Stop();
                
        /// <summary>
        /// Setting this will set the <see cref="Entity.Destination"/> for the Pathfinding Component and handle the destination updating for <see cref="Follow"/>.
        /// </summary>
        public Target? Destination { get; set; }
        public float Distance { get; }
    }
}
