using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using System;
using UnityEngine;
namespace _Game.Scripts
{
    [Serializable]
    public class CellStats : Stats, IAliveStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(500, 0, 500);
    }
}