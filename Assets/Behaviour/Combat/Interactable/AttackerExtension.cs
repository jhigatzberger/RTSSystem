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
        public override void Enable()
        {
            targeter = Entity.GetExtension<ITargeter>();
            targeter.OnTargetEvent += Targeter_OnTargetEvent;
        }

        private void Targeter_OnTargetEvent(Target? obj)
        {
            if (Entity.MonoBehaviour.enabled && Entity.MonoBehaviour.gameObject.activeSelf)
                anim.SetLayerWeight(1, obj == null ? 0 : 1);
        }
        #region Stats
        public int Damage { get => Properties.damage; }
        public float AttackCooldown { get => Properties.attackCoolDown; }
        public float VisionRange { get => Properties.visionRange; }
        public float AttackRange { get => Properties.attackRange; }
        public bool IsInRange {
            get =>
                Target != null &&
                Target.Value.HasActiveEntity &&
                Target.Value.IsInRange(Entity.Position, AttackRange);
        }
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
            anim.SetInteger("ABAnim", ABAnim++);
            lastAttack = LockStep.time;
            if (Target.HasValue)
            {
                IAttackable attackable = Target.Value.entity.GetExtension<IAttackable>();
                if (attackable.IsAlive)
                    attackable.Health -= Damage;
                else
                    targeter.Target = null;
            }
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
        #endregion

        #region Target
        private ITargeter targeter;
        public Target? Target
        {
            get => targeter.Target;
        }
        public override void Clear()
        {
            targeter.Target = null;
            if (attackCoroutine != null)
            {
                Entity.MonoBehaviour.StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
        public override void Disable()
        {
            anim.SetLayerWeight(1, 0);
            targeter.Target = null;
            targeter.OnTargetEvent -= Targeter_OnTargetEvent;
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
