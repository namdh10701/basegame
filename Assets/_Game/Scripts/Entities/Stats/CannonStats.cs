using System;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class CannonStats : Stats, IAliveStats, IFighterStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(500, 0, 800);

        [field: SerializeField]
        public Stat BlockChance { get; set; } = new();

        [field: SerializeField]
        public Stat AttackDamage { get; set; } = new("AttackDamage");

        [field: SerializeField]
        public Stat CriticalChance { get; set; } = new();

        [field: SerializeField]
        public Stat CriticalDamage { get; set; } = new();

        [field: SerializeField]
        public Stat AttackRange { get; set; } = new();

        [field: SerializeField]
        public Stat AttackAccuracy { get; set; } = new();

        [field: SerializeField]
        public RangedStat Ammo { get; set; } = new(10, 0, 10);

        [field: SerializeField]
        public Stat AttackSpeed { get; set; } = new();
        [field: SerializeField]
        public Stat ProjectileCount { get; set; } = new();

        [field: SerializeField]
        public Stat PrimaryDamage { get; set; } = new();
        [field: SerializeField]
        public Stat SecondaryDamage { get; set; } = new();

        public IFighterStats.AttackTypes AttackType { get; set; } = IFighterStats.AttackTypes.UNIT;

    }
}