using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.GD;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public abstract class Enemy : Entity, IEffectTaker, ISlowable
    {
        [Header("Enemy")]
        [SerializeField] string enemyId;
        [SerializeField] protected EnemyStats _stats;
        [SerializeField] private EnemyStatsTemplate _statsTemplate;

        [Header("Behaviour")]
        [SerializeField] public EffectTakerCollider EffectTakerCollider;
        [SerializeField] protected EffectHandler effectHandler;
        [SerializeField] protected Collider2D pushCollider;
        [SerializeField] protected Blackboard blackboard;
        [SerializeField] protected MBTExecutor MBTExecutor;

        [SerializeField] protected SpineAnimationEnemyHandler spineAnimationEnemyHandler;
        public override Stats Stats => _stats;
        public Transform Transform => transform;
        public EffectHandler EffectHandler => effectHandler;

        public List<Stat> SlowableStats => new List<Stat>() { _stats.MoveSpeed, _stats.AnimationTimeScale };
        public ObjectCollisionDetector FindTargetCollider;
        protected virtual IEnumerator Start()
        {
            EffectTakerCollider.Taker = this;
            Ship ship = FindAnyObjectByType<Ship>();
            if (ship == null || blackboard == null)
                yield break;
            blackboard.GetVariable<ShipVariable>("Ship").Value = ship;
            MBTExecutor.enabled = false;
            yield return StartActionCoroutine();
            MBTExecutor.enabled = true;
        }
        public virtual void Die()
        {
            body.velocity = Vector3.zero;
            pushCollider.enabled = false;
            EffectTakerCollider.gameObject.SetActive(false);
            MBTExecutor.gameObject.SetActive(false);
        }

        public abstract IEnumerator StartActionCoroutine();
        public abstract bool IsReadyToAttack();
        public abstract IEnumerator AttackSequence();
        public abstract void Move();

        protected override void LoadStats()
        {
            if (GDConfigLoader.Instance == null)
            {
                _statsTemplate.ApplyConfig(_stats);
            }
            else
            {
                if (GDConfigLoader.Instance.Enemies.TryGetValue(enemyId, out EnemyConfig enemyConfig))
                {
                    enemyConfig.ApplyGDConfig(_stats);
                }
                else
                {
                    _statsTemplate.ApplyConfig(_stats);
                }
            }
        }

        protected override void ApplyStats()
        {
            FindTargetCollider?.SetRadius(_stats.AttackRange.Value);
        }

        protected override void LoadModifiers()
        {

        }

        public virtual void OnSlowed()
        {
            spineAnimationEnemyHandler.OnSlowedDown();
        }

        public virtual void OnSlowEnded()
        {
            spineAnimationEnemyHandler.OnSlowEnded();
        }
    }
}
