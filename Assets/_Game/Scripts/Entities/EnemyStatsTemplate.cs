using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
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
        }
    }
}