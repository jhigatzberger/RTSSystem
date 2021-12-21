using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    public interface IAttacker: IEntityExtension
    {
        public int Team { get; set; }
        public IAttackable Target { get; set; }
        public bool IsInRange { get; }
        public float VisionRange { get; set; }
        public float AttackRange { get; set; }
        public int Damage { get; set; }
        public float AttackSpeed { get; set; }
        public bool CanAttack { get; }
        public void Attack();

    }
}
