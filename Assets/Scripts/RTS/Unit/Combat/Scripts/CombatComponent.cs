using RTSEngine.Entity;
using RTSEngine.Entity.AI;
using System.Collections;
using UnityEngine;

public class CombatComponent : MonoBehaviour, IAttackable, IAttacker
{
    private BaseEntity _entity;
    public BaseEntity Entity => _entity;
    private Animator anim;
    private void Awake()
    {
        _entity = GetComponent<BaseEntity>();
        anim = GetComponent<Animator>();
    }
    [SerializeField] private int _team;
    public int Team { get => _team; set { _team = value; } }
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
    public bool IsAlive => Health>0;
    [SerializeField] private int _damage;
    public int Damage { get => _damage; set { _damage = value; } }
    [SerializeField] private float _attackSpeed;
    public float AttackSpeed { get => _attackSpeed; set { _attackSpeed = value; } }
    private IAttackable _target;
    public IAttackable Target { get => _target;
        set
        {
            if(value != _target)
            {
                _target = value;
                StartCoroutine(TransitionCombatLayer(value==null?1:0, value == null ? 0 : 1, 3));
            }
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

    public bool IsInRange { get => Target != null && Target.IsAlive && Vector3.Distance(Entity.transform.position, Target.Entity.transform.position)<=AttackRange; }
    [SerializeField] private float _visionRange;
    public float VisionRange { get => _visionRange; set { _visionRange = value; } }
    [SerializeField] private float _attackRange;
    public float AttackRange { get => _attackRange; set { _attackRange = value; } }
    public bool CanAttack => LockStep.time - lastAttack > 1/AttackSpeed && IsInRange;
    public void Die()
    {
        Entity.enabled = false;
        anim.SetTrigger("Death");
    }
    private float lastAttack = 0;
    public void Attack()
    {
        Target.Health -= Damage;
        lastAttack = LockStep.time;
        anim.SetTrigger("Attack");
        if (!Target.IsAlive)
        {
            Target = null;
        }
    }

    public void OnExitScene()
    {
        enabled = false;
    }
}


