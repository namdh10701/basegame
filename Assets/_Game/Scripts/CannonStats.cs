using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class CannonStats : Stats, IAliveStats, IFighterStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new();

        [field: SerializeField]
        public Stat AttackDamage {get;set;} = new();
        
        [field: SerializeField]
        public Stat CriticalChance {get;set;} = new();
        
        [field: SerializeField]
        public Stat CriticalDamage {get;set;} = new();

        [field: SerializeField]
        public Stat AttackRange { get; set; } = new();

        public Stat AttackAccuracy { get; set; } = new();

        public IFighterStats.AttackTypes AttackType { get; set; } = IFighterStats.AttackTypes.UNIT;

        // [SerializeField]
        // private float _healthPoint;
        // float IAliveStats.HealthPoint
        // {
        //     get => _healthPoint;
        //     set => _healthPoint = value;
        // }
    }
}