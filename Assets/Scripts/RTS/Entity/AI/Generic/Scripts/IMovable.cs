using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity
{
    public interface IMovable : IEntityExtension
    {
        public void Enqueue(BaseEntity entity);
        public void Enqueue(Vector3 destination);
        public void Move();
        public void Stop();
        public void Clear();
        public Destination Destination { get; set; }
    }
    public class Destination
    {
        public Vector3 point;
        public virtual Vector3 Point => point;
    }
    public class Follow : Destination
    {
        public BaseEntity entity;
        public override Vector3 Point => entity.transform.position;
    }
}
