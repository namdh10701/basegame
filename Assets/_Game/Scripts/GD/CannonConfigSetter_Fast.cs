using _Game.Scripts.Entities;
using UnityEngine;

namespace _Game.Scripts.GD
{
    public class CannonConfigSetter_Fast : MonoBehaviour
    {
        // public CannonConfig cannonConfig;
        public Cannon Cannon;

        private void Awake()
        {
            // var cannonStats = (Cannon.Stats as CannonStats)!;
            // cannonStats.HealthPoint.MaxStatValue.BaseValue = cannonConfig.HP;
            // cannonStats.HealthPoint.StatValue.BaseValue = cannonConfig.HP;
            // cannonStats.AttackAccuracy.BaseValue = cannonConfig.accuracy;
            // cannonStats.AttackDamage.BaseValue = cannonConfig.attack;
            // cannonStats.AttackRange.BaseValue = cannonConfig.range;
            // cannonStats.CriticalChance.BaseValue = cannonConfig.crit_chance;
            // cannonStats.CriticalDamage.BaseValue = cannonConfig.crit_damage;
        }
    }
}