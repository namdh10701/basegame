using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using System;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    [Serializable]
    public class AmmoStats : Stats, IAliveStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(500, 0, 800);

        [field: SerializeField]
        public Stat EnergyCost;
        [field: SerializeField]
        public Stat MagazineSize;
        [field: SerializeField]
        public Stat ProjectileCount;
    }
}