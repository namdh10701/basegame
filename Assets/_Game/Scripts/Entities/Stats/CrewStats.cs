using System;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class CrewStats : Stats
    {
        [field: SerializeField]
        public Stat MoveSpeed { get; set; } = new();

        [field: SerializeField]
        public Stat RepairSpeed { get; set; } = new();

        [field: SerializeField]
        public Stat StatusReduce { get; set; } = new();

        [field: SerializeField]
        public Stat ZeroManaCost { get; set; } = new();

        [field: SerializeField]
        public Stat Luck { get; set; } = new();

        [field: SerializeField]
        public Stat BonusAmmo { get; set; } = new();

        [field: SerializeField]
        public Stat FeverTimeProb { get; set; } = new();
        [field: SerializeField]
        public Stat GoldIncome { get; set; } = new();
    }
}