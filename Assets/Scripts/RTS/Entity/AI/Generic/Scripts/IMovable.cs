using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity
{
    public interface IMovable : IEntityExtension
    {
        public void Enqueue(Vector3 destination);
        public void Move();
        public void Stop();
        public void Clear();
        public Vector3? Destination { get; set; }
    }

}
