using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class CannonStats : GridItemStats, IFighterStats
    {
        [field: SerializeField]
        public Stat AttackDamage { get; set; } = new("AttackDamage");

        [field: SerializeField]
        public Stat CriticalChance { get; set; } = new();

        [field: SerializeField]
        public Stat CriticalDamage { get; set; } = new();

        [field: SerializeField]
        public Stat AttackRange { get; set; } = new();

        public Stat AttackAccuracy { get; set; } = new();

        public RangedStat Ammo { get; set; } = new(10, 0, 10);

        public IFighterStats.AttackTypes AttackType { get; set; } = IFighterStats.AttackTypes.UNIT;
    }
}