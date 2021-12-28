using RTSEngine.Entity;
using RTSEngine.Entity.Combat;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseEntity))]
public class CombatEntity : MonoBehaviour, IAttackable, IAttacker
{
    #region Stats
    [SerializeField] private CombatStats stats;
    public int Damage { get => stats.Damage; }
    public float AttackCooldown { get => stats.AttackCooldown; }
    public float VisionRange { get => stats.VisionRange; }
    public float AttackRange { get => stats.AttackRange; }
    public float DamageDelay => stats.DamageDelay;
    public bool IsInRange { get => Target != null && Target.IsAlive && Vector3.Distance(Entity.transform.position, Target.Entity.transform.position) <= AttackRange; }
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
        attackCoroutine = StartCoroutine(PerformAttack());
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
                if(enabled && gameObject.activeSelf)
                    StartCoroutine(TransitionCombatLayer(value == null ? 1 : 0, value == null ? 0 : 1, 3));
            }
        }
    }
    public void ResetTarget()
    {
        Target = null;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
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

    #region Attackable
    [SerializeField] private int _health;
    public int Health
    {
        get => _health;
        set
        {
            if (IsAlive)
            {
                _health = value;
                if (_health <= 0)
                    Die();
            }
        }
    }
    public bool IsAlive => Health > 0;
    public void Die()
    {
        Target = null;
        Entity.enabled = false;
        anim.SetTrigger("Death");
    }
    #endregion
    private BaseEntity _entity;
    public BaseEntity Entity => _entity;
    private Animator anim;
    private void OnEnable()
    {
        _health = stats.Health;
        _entity = GetComponent<BaseEntity>();
        anim = GetComponent<Animator>();
        Entity.OnClear += ResetTarget;
    }
    public void OnExitScene()
    {
        Entity.OnClear -= ResetTarget;
        enabled = false;
    }
}