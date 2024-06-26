using System;
using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Attributes;
using _Game.Scripts.Battle;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.GD;
using MBT;
using Mono.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace _Game.Scripts.Entities
{
    public abstract class Enemy : Entity, IEffectTaker
    {
        [Header("Enemy")]
        [SerializeField] string                         enemyId;
        [SerializeField] protected EnemyStats           _stats;
        [SerializeField] private EnemyStatsTemplate     _statsTemplate;

        [Header("Behaviour")]
        [SerializeField] protected EffectTakerCollider  EffectTakerCollider;
        [SerializeField] protected EffectHandler        effectHandler;
        [SerializeField] protected Collider2D           pushCollider;
        [SerializeField] protected Blackboard           blackboard;
        [SerializeField] protected MBTExecutor          MBTExecutor;

        public override Stats Stats => _stats;
        public Transform Transform => transform;
        public EffectHandler EffectHandler => effectHandler;

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
            pushCollider.enabled = false;
            EffectTakerCollider.GetComponent<Collider2D>().enabled = false;
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
            }
        }

        protected override void ApplyStats()
        {

        }

        protected override void LoadModifiers()
        {

        }
    }
}
