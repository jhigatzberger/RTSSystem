using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.Combat
{
    public interface IAttackable : IEntityExtension
    {
        public int Health { get; set; }
        public bool IsAlive { get; }
        public void Die();
    }
}
