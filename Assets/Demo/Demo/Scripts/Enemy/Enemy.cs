using Demo.ScriptableObjects.Scripts;
using Demo.Scripts.Canon;
using Demo.Scripts.Defense;
using MBT;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public EnemyData EnemyData;
        [SerializeField] protected Blackboard blackboard;
        protected Transform target;
        public CooldownBrain Cooldown;
        public Rigidbody2D body;
        public BoxCollider2D collider;
        public bool IsPlayerInRange;
        DefenseBehaviour defenseBehaviour;
        public bool IsAbleToAttack => !Cooldown.IsInCooldown && IsPlayerInRange;
        protected virtual void Start()
        {
            defenseBehaviour = GetComponent<DefenseBehaviour>();
            Cooldown.SetCooldownTime(1 / EnemyData.AttackSpeed);
            target = GameObject.Find("Ship").transform;
        }

        public virtual void DoAttack()
        {
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("PlayerBullet"))
            {
                Destroy(collision.gameObject);
                defenseBehaviour.DefenseData.Hp -= 100;
                if (defenseBehaviour.DefenseData.Hp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}