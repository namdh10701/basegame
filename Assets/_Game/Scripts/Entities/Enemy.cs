using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Attributes;
using UnityEngine;

namespace _Game.Scripts
{
    public class Enemy : Entity
    {
        [SerializeField]
        private EnemyStats stats;
        public override Stats Stats => stats;

        public HealthPoint HealthPoint { get; set; }
    }
}
