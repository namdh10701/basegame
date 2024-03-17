using MBT;
using UnityEngine;
using UnityEngine.Events;
public abstract class Enemy : MonoBehaviour
{   
    public EnemyData EnemyData;
    [SerializeField] protected Blackboard blackboard;
    protected Transform target;
    public CooldownBrain Cooldown;
    public Rigidbody2D body;
    public BoxCollider2D collider;
    public bool IsPlayerInRange;
    public bool IsAbleToAttack => !Cooldown.IsInCooldown && IsPlayerInRange;
    protected virtual void Start()
    {
        Cooldown.SetCooldownTime(1 / EnemyData.AttackSpeed);
        target = GameObject.Find("Ship").transform;
    }

    public virtual void DoAttack()
    {
    }
}