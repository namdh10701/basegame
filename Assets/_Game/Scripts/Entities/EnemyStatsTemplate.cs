using System;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/Enemy Stats", order = 1)]
    [Serializable]
    public class EnemyStatsTemplate : ScriptableObject
    {
        [field: SerializeField] public EnemyStats Data { get; set; } = new();
        public void ApplyConfig(EnemyStats enemyStats)
        {
            enemyStats.AttackDamage.BaseValue = Data.AttackDamage.BaseValue;
            enemyStats.AttackRange.BaseValue = Data.AttackRange.BaseValue;
            enemyStats.HealthPoint.MaxStatValue.BaseValue = Data.HealthPoint.MaxStatValue.BaseValue;
            enemyStats.HealthPoint.StatValue.BaseValue = Data.HealthPoint.StatValue.BaseValue;
            enemyStats.ActionSequenceInterval.BaseValue = Data.ActionSequenceInterval.Value;
        }
    }
}