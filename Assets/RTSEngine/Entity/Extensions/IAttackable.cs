using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.AI
{
    public interface IAttackable : IEntityExtension
    {
        public int Team { get; set; }
        public int Health { get; set; }
        public bool IsAlive { get; }
        public void Die();
    }
}
