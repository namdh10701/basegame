using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts
{
    public class Enemy : Entity, IShooter
    {
        [SerializeField]
        private EnemyStats _stats;

        [SerializeField]
        private EnemyStatsTemplate _statsTemplate;
        public override Stats Stats => _stats;

        public HealthPoint HealthPoint { get; set; }
        public IFighterStats FighterStats { get; set; }
        public AttackStrategy AttackStrategy { get; set; }
        public List<Effect> BulletEffects { get; set; }

        private void Start()
        {
            // _stats = Instantiate(_statsTemplate).Data;
            // var eff = gameObject.AddComponent<DecreaseHealthEffect>();
            // eff.Amount = _stats.AttackDamage.Value;
            // BulletEffects.Add(eff);
            //
            // _stats.AttackDamage.OnValueChanged += stat =>
            // {
            //     eff.Amount = stat.Value;
            // };
        }
    }
}
