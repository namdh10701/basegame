using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Features.Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Scripts
{
    [Serializable]
    public class CellStats : Stats, IAliveStats, IShieldable
    {
        public ShipStats ShipStats { get; set; }
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(200, 0, 200);
        public List<RangedStat> Shields { get => ShipStats.Shields; set { return; } }
        public List<RangedStat> Blocks { get => ShipStats.Blocks; set { return; } }
    }
}