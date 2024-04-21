using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using Demo.ScriptableObjects.Scripts;
using Demo.Scripts.Canon;
using Demo.Scripts.Defense;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public abstract class Enemy : Entity
    {
        public EnemyData EnemyData;
        protected Transform target;
        public CooldownBrain Cooldown;
        public Rigidbody2D body;
        public BoxCollider2D collider;
        public bool IsPlayerInRange;
        DefenseBehaviour defenseBehaviour;
        public CellPattern cellPattern;
        public CannonStats stats = new();
        public override _Game.Scripts.Stats Stats => stats;


        protected List<Cell> targetCells = new List<Cell>();
        protected GridAttackHandler gridAttackHandler;
        protected GridPicker gridPicker;
        protected Cell centerCell;
        public virtual bool IsAbleToAttack => !Cooldown.IsInCooldown && IsPlayerInRange;
        protected Coroutine attackCoroutine;

        protected virtual void Start()
        {
            gridPicker = FindAnyObjectByType<GridPicker>();
            gridAttackHandler = FindAnyObjectByType<GridAttackHandler>();
            defenseBehaviour = GetComponent<DefenseBehaviour>();
            Cooldown.SetCooldownTime(1 / EnemyData.AttackSpeed);
            target = GameObject.Find("Ship").transform;
        }

        protected virtual void Update()
        {
            if (IsAbleToAttack && attackCoroutine == null)
            {
                Debug.Log("ATTACK");
                attackCoroutine = StartCoroutine(AttackCoroutine());
            }
        }

        public virtual void DoAttack()
        {

        }
        public virtual void DoTarget()
        {
        }

        IEnumerator AttackCoroutine()
        {
            DoTarget();
            yield return new WaitForSeconds(1.5f);
            DoAttack();
            attackCoroutine = null;
            yield break;
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