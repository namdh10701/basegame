using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Attributes;
using _Game.Scripts.Battle;
using _Game.Scripts.Gameplay.Ship;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public abstract class Enemy : Entity, IShooter
    {
        [SerializeField]
        protected EnemyStats _stats;

        [SerializeField]
        private EnemyStatsTemplate _statsTemplate;

        [SerializeField]
        protected Blackboard _blackboard;


        public override Stats Stats => _stats;

        public HealthPoint HealthPoint { get; set; }
        public IFighterStats FighterStats { get; set; }
        public AttackStrategy AttackStrategy { get; set; }
        public List<Effect> BulletEffects { get; set; }

        [SerializeField] EnemyAttackBehaviour EnemyAttackBehaviour;

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
        }

        public void DoAttack()
        {
            EnemyAttackBehaviour.DoAttack();
        }
    }
}
