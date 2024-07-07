﻿using System;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Cannon_", menuName = "SO/Cannon", order = 1)]
    [Serializable]
    public class CannonConfig : GDConfig, IOperationConfig
    {
        public string id;
        public string operation_type;
        public string rarity;
        public string rarity_level;
        public string name;
        public string default_rarity;
        public float hp;
        public float attack;
        public float attack_speed;
        public float accuracy;
        public float crit_chance;
        public float crit_damage;
        public float range;
        public float skill;
        public float project_count;

        public string OperationType { get => operation_type; set => operation_type = value; }

        public override string GetId() => id;

        public override void ApplyGDConfig(object stats)
        {
            var cannonSt = (stats as CannonStats)!;
            cannonSt.HealthPoint.StatValue.BaseValue = hp;
            cannonSt.HealthPoint.MaxStatValue.BaseValue = hp;
            cannonSt.AttackDamage.BaseValue = attack;
            cannonSt.AttackAccuracy.BaseValue = accuracy;
            cannonSt.CriticalChance.BaseValue = crit_chance;
            cannonSt.CriticalDamage.BaseValue = crit_damage;
            cannonSt.AttackSpeed.BaseValue = attack_speed;
            cannonSt.AttackRange.BaseValue = range;
            cannonSt.Ammo.MaxStatValue.BaseValue = 10;
            cannonSt.Ammo.StatValue.BaseValue = 10;
            cannonSt.ProjectileCount.BaseValue = project_count;
        }
    }
}