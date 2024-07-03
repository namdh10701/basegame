using System;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Enemy_", menuName = "SO/Enemy", order = 1)]
    [Serializable]
    public class EnemyConfig : GDConfig
    {
        public string id;
        public string name;
        public float attack;
        public float attack_speed;
        public float power_number;
        public float hp;
        public float block_chance;
        public float attack_range;

        public override string GetId() => id;

        public override void ApplyGDConfig(object stats)
        {
            EnemyStats enemyStats = (EnemyStats)stats;
            enemyStats.HealthPoint.MaxStatValue.BaseValue = hp;
            enemyStats.HealthPoint.StatValue.BaseValue = hp;
            enemyStats.AttackDamage.BaseValue = attack;
            enemyStats.BlockChance.BaseValue = block_chance;
            enemyStats.ActionSequenceInterval.BaseValue = attack_speed;
            enemyStats.AttackRange.BaseValue = attack_range;
        }
    }
}