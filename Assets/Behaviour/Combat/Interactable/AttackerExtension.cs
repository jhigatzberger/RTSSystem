using JHiga.RTSEngine;
using System.Collections;
using UnityEngine;

namespace JHiga.RTSEngine.Combat
{
    public class AttackerExtension : BaseInteractableExtension<AttackerProperties>, IAttacker
    {
        private Animator anim;
        public AttackerExtension(IExtendableEntity extendable, AttackerProperties properties) : base(extendable, properties)
        {
            anim = Entity.MonoBehaviour.GetComponent<Animator>();
        }

        #region Stats
        public int Damage { get => Properties.damage; }
        public float AttackCooldown { get => Properties.attackCoolDown; }
        public float VisionRange { get => Properties.visionRange; }
        public float AttackRange { get => Properties.attackRange; }
        public float DamageDelay => Properties.damageDelay;
        public bool IsInRange { get => Target != null && Target.IsAlive && Vector3.Distance(Entity.MonoBehaviour.transform.position, Target.Entity.MonoBehaviour.transform.position) <= AttackRange; }
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
            attackCoroutine = Entity.MonoBehaviour.StartCoroutine(PerformAttack());
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
                    if (Entity.MonoBehaviour.enabled && Entity.MonoBehaviour.gameObject.activeSelf)
                        Entity.MonoBehaviour.StartCoroutine(TransitionCombatAnimLayer(value == null ? 1 : 0, value == null ? 0 : 1, 3));
                }
            }
        }
        public override void Clear()
        {
            Target = null;
            if (attackCoroutine != null)
            {
                Entity.MonoBehaviour.StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }

        public override void Disable()
        {
            anim.SetLayerWeight(1, 0);
            _target = null;
            if (attackCoroutine != null)
            {
                Entity.MonoBehaviour.StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }

        private IEnumerator TransitionCombatAnimLayer(float startWeight, float endWeight, float speed) // maybe remove! performance ^^
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
    }
}
