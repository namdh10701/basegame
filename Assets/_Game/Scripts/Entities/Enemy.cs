using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Attributes;
using UnityEngine;

namespace _Game.Scripts
{
    public class Enemy : Entity, IShooter
    {
        [SerializeField]
        private EnemyStats stats;
        public override Stats Stats => stats;

        public HealthPoint HealthPoint { get; set; }
        public IFighterStats FighterStats { get; set; }
        public AttackStrategy AttackStrategy { get; set; }
        public List<Effect> BulletEffects { get; set; }
    }
}
