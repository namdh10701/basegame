using System;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class ShipBuffStats : Stats
    {
        [field: SerializeField]
        public Stat ZeroManaCost { get; set; } = new();

        [field: SerializeField]
        public Stat Luck { get; set; } = new();

        //double ammo chance
        [field: SerializeField]
        public Stat BonusAmmo { get; set; } = new();

        [field: SerializeField]
        public Stat FeverTimeProb { get; set; } = new();

        //%
        [field: SerializeField]
        public Stat GoldIncome { get; set; } = new();

        [field: SerializeField]
        public Stat LifeSteal { get; set; } = new();

        [field: SerializeField]
        public Stat AmmoEnergyCostReduce { get; set; } = new();

        [field: SerializeField]
        public Stat CrewRepairSpeedBoost { get; set; } = new();
        [field: SerializeField]
        public Stat Revive { get; set; } = new();
        [field: SerializeField]
        public Stat DmgIncrease { get; set; } = new();
    }
}