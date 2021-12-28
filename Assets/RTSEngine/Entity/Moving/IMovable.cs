using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity
{
    public interface IMovable : IEntityExtension
    {
        /// <summary>
        /// Instantiates a <see cref="Follow"/> and enqueues it for later <see cref="IMovable.Move"/>, if there is no other <see cref="IMovable.Destination"/> in queue, <see cref="IMovable.Move"/>.
        /// </summary>
        public void Enqueue(BaseEntity entity);
        
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
        /// Clear destinations and <see cref="IMovable.Stop"/>.
        /// </summary>
        public void Clear();
        
        /// <summary>
        /// Setting this will set the <see cref="Entity.Destination"/> for the Pathfinding Component and handle the destination updating for <see cref="Follow"/>.
        /// </summary>
        public Destination Destination { get; set; }
    }
    
    /// <summary>
    /// Required to Enqueue either a point in the world or an <see cref="BaseEntity"/> that may change position.
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
    /// Derivative of <see cref="Destination"/> that holds a reference to a <see cref="BaseEntity"/> position.
    /// </summary>
    public class Follow : Destination
    {
        public BaseEntity entity;       
        public override Vector3 Point => entity.transform.position;
    }
}
