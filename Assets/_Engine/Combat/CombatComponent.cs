using JHiga.RTSEngine;
using System.Collections;
using UnityEngine;

namespace JHiga.RTSEngine.Combat
{
    public class CombatComponent : AttackableComponent, IAttacker
    {
        private Animator anim;
        public CombatComponent(IExtendable extendable, CombatStats stats) : base(extendable, stats)
        {
            anim = Extendable.MonoBehaviour.GetComponent<Animator>();
        }

        #region Stats
        public int Damage { get => stats.Damage; }
        public float AttackCooldown { get => stats.AttackCooldown; }
        public float VisionRange { get => stats.VisionRange; }
        public float AttackRange { get => stats.AttackRange; }
        public float DamageDelay => stats.DamageDelay;
        public bool IsInRange { get => Target != null && Target.IsAlive && Vector3.Distance(Extendable.MonoBehaviour.transform.position, Target.Extendable.MonoBehaviour.transform.position) <= AttackRange; }
        #endregion

        #region Attack
        public bool CanAttack => LockStep.time - lastAttack > AttackCooldown && IsInRange;
        public bool IsAttacking => attackCoroutine != null;
        private Coroutine attackCoroutine = null;
        private float lastAttack = 0;
        public void Attack()
        {
            if (!CanAttack)
                return;
            attackCoroutine = Extendable.MonoBehaviour.StartCoroutine(PerformAttack());
        }
        private int abAnim = 0;
        private int ABAnim
        {
            get => abAnim;
            set
            {
                abAnim = value + 2 % 2;
            }
        }
        IEnumerator PerformAttack()
        {
            anim.SetInteger("ABAnim", ABAnim++);
            anim.SetTrigger("Attack");
            lastAttack = LockStep.time;
            yield return new WaitForSeconds(DamageDelay);
            if (Target != null && Target.IsAlive)
            {
                Target.Health -= Damage;
            }
            else
                Target = null;
            attackCoroutine = null;
        }
        #endregion

        #region Target
        private IAttackable _target;
        public IAttackable Target
        {
            get => _target;
            set
            {
                if (value != _target)
                {
                    _target = value;
                    if (Extendable.MonoBehaviour.enabled && Extendable.MonoBehaviour.gameObject.activeSelf)
                        Extendable.MonoBehaviour.StartCoroutine(TransitionCombatLayer(value == null ? 1 : 0, value == null ? 0 : 1, 3));
                }
            }
        }
        public override void Clear()
        {
            Target = null;
            if (attackCoroutine != null)
            {
                Extendable.MonoBehaviour.StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }

        private IEnumerator TransitionCombatLayer(float startWeight, float endWeight, float speed) // maybe remove! performance ^^
        {
            float weight = startWeight;
            while (weight < endWeight)
            {
                weight += Time.deltaTime * speed;
                anim.SetLayerWeight(1, weight);
                yield return null;
            }
            anim.SetLayerWeight(1, endWeight);
        }
        #endregion

        public override void Die()
        {
            Target = null;
            Extendable.MonoBehaviour.enabled = false;
            anim.SetTrigger("Death");
        }
    }
}
