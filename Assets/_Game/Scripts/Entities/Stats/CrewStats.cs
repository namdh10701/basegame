using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    // [CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/Enemy Stats", order = 1)]
    [Serializable]
    public class CrewStats : Stats, IAliveStats
    {
        [field: SerializeField]
        public RangedStat HealthPoint { get; set; } = new(500, 0, 800);

        [field: SerializeField]
        public Stat BlockChance { get; set; } = new();

        [field: SerializeField]
        public Stat MoveSpeed { get; set; } = new();

        [field: SerializeField]
        public Stat EvadeChance { get; set; } = new();
    }
}