using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class ShipStats : Stats, IAliveStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(0, 0, 100);

        [field: SerializeField]
        public Stat BlockChance { get; set; } = new();

        [field: SerializeField]
        public Stat CannonLimit { get; set; } = new();

        [field: SerializeField]
        public Stat AmmoLimit { get; set; } = new();

        [field: SerializeField]
        public RangedStat ManaPoint { get; set; } = new(0, 0, 150);
        
        [field: SerializeField]
        public RangedStat ManaRegenerationRate { get; set; } = new(5, 0, 10);
        
        [field: SerializeField]
        public RangedStat HealthRegenerationRate { get; set; } = new(1, 0, 10);

    }
}