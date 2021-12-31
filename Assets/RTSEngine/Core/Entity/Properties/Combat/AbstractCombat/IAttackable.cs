using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.Combat
{
    public interface IAttackable : IExtension
    {
        public int Health { get; set; }
        public bool IsAlive { get; }
        public void Die();
    }
}
