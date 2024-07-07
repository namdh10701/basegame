using System;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(fileName = "CannonStats", menuName = "Scriptable Objects/Cannon Stats", order = 1)]
    [Serializable]
    public class CannonStatsTemplate : StatsTemplate
    {
        [field: SerializeField] public CannonStats Data { get; set; } = new();

        public override void ApplyConfig(Stats cannonStats)
        {
            CannonStats stats = cannonStats as CannonStats;
            stats.BlockChance.BaseValue = Data.BlockChance.BaseValue;
            stats.CriticalChance.BaseValue = Data.CriticalChance.BaseValue;
            stats.AttackAccuracy.BaseValue = Data.AttackAccuracy.BaseValue;
            stats.AttackSpeed.BaseValue = Data.AttackSpeed.BaseValue;
            stats.AttackDamage.BaseValue = Data.AttackDamage.BaseValue;
            stats.Ammo.StatValue.BaseValue = Data.Ammo.StatValue.BaseValue;
            stats.Ammo.MinStatValue.BaseValue = Data.Ammo.MinStatValue.BaseValue;
            stats.Ammo.MaxStatValue.BaseValue = Data.Ammo.MaxStatValue.BaseValue;
            stats.AttackRange.BaseValue = Data.AttackRange.BaseValue;
            stats.CriticalDamage.BaseValue = Data.CriticalDamage.BaseValue;
            stats.AttackType = Data.AttackType;

        }
    }
}