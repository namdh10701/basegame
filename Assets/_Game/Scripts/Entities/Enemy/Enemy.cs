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
using MBT;
using Spine.Unity;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace _Game.Scripts.Entities
{
    public abstract class Enemy : Entity
    {
        [SerializeField]
        protected EnemyStats _stats;

        [SerializeField]
        private EnemyStatsTemplate _statsTemplate;



        public override Stats Stats => _stats;

        public HealthPoint HealthPoint { get; set; }
        public IFighterStats FighterStats { get; set; }
        public List<Effect> BulletEffects { get; set; }

        [Header("Behaviour")]
        [SerializeField] protected Blackboard _blackboard;
        [SerializeField] protected Rigidbody2D body;
        [SerializeField] protected Collider2D collider;
        [SerializeField] EnemyAttackBehaviour EnemyAttackBehaviour;
        [SerializeField] protected SpineAnimationEnemyHandler SpineAnimationEnemyHandler;
        [SerializeField] MBTExecutor MBTExecutor;
        protected virtual void Start()
        {
            Ship ship = FindAnyObjectByType<Ship>();
            if (ship == null)
            {
                return;
            }
            if (_blackboard == null)
                return;

            _blackboard.GetVariable<ShipVariable>("Ship").Value = ship;
            _blackboard.GetVariable<FloatVariable>("ActionSequenceInterval").Value = _stats.ActionSequenceInterval.Value;

            _stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
        }
        private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat obj)
        {
            if (obj.StatValue.Value <= obj.MinValue)
            {

                Debug.LogError("DIE");
                Die();
            }
        }
        public abstract IEnumerator Teleport(Vector2 pos);

        public virtual void Die()
        {
            collider.enabled = false;
            MBTExecutor.enabled = false;
            StopAllCoroutines();
            EnemyAttackBehaviour.StopAllCoroutines();
            SpineAnimationEnemyHandler.PlayAnim(Anim.Dead, false, () => Destroy(gameObject));
        }
    }
}
