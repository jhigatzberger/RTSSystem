using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public interface IMovable
    {
        public void MoveTo(MoveCommand command);
        public IEnumerator MoveToAsync(MoveCommand command);
        public void Stop();
    }
}

