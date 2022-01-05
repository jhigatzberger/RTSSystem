using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.Movement
{
    public interface IMovable : IExtension
    {
        /// <summary>
        /// Instantiates a <see cref="Follow"/> and enqueues it for later <see cref="IMovable.Move"/>, if there is no other <see cref="IMovable.Destination"/> in queue, <see cref="IMovable.Move"/>.
        /// </summary>
        public void Enqueue(Transform follow);
        
        /// <summary>
        /// Instantiates a <see cref="Entity.Destination"/> and enqueues it for later <see cref="IMovable.Move"/>, if there is no other <see cref="IMovable.Destination"/> in queue, <see cref="IMovable.Move"/>.
        /// </summary>
        public void Enqueue(Vector3 destination);
        
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
        public Destination Destination { get; set; }
    }
    
    /// <summary>
    /// Required to Enqueue either a point in the world or an <see cref="GameEntity"/> that may change position.
    /// </summary>
    public class Destination
    {
        public Vector3 point;
        
        /// <summary>
        /// Holds the reference to the destination for the Pathfinding Component.
        /// </summary>
        public virtual Vector3 Point => point;
    }
    
    /// <summary>
    /// Derivative of <see cref="Destination"/> that holds a reference to a <see cref="GameEntity"/> position.
    /// </summary>
    public class Follow : Destination
    {
        public Transform toFollow;       
        public override Vector3 Point => toFollow.position;
    }
}
