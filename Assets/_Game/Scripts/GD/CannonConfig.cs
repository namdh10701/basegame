using System;
using System.Reflection;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Cannon_", menuName = "SO/Cannon", order = 1)] 
    [Serializable]
    public class CannonConfig: GDConfig
    {
        public string ID;
        public string name;
        public string default_rarity;
        public float HP;
        public float attack;
        public float attack_speed;
        public float accuracy;
        public float crit_chance;
        public float crit_damage;
        public float range;
        public float skill;

        public override string GetId() => ID;

        public override void ApplyGDConfig(object stats)
        {
            // var gdConfig = GDConfigLoader.Instance.CannonMap[ID];
            // foreach (var fieldInfo in typeof(CannonConfig).GetFields(BindingFlags.Public | BindingFlags.Instance))
            // {
            //     object value = gdConfig[fieldInfo.Name];
            //     if (fieldInfo.FieldType == typeof(float))
            //     {
            //         value ??= 0f;
            //     }
            //     fieldInfo.SetValue(this, value);
            // }

            var cannonSt = (stats as CannonStats)!;
            cannonSt.HealthPoint.StatValue.BaseValue = HP;
            cannonSt.HealthPoint.MaxStatValue.BaseValue = HP;
            cannonSt.AttackDamage.BaseValue = attack;
            cannonSt.AttackDamage.BaseValue = attack_speed;
            cannonSt.AttackAccuracy.BaseValue = accuracy;
            cannonSt.CriticalChance.BaseValue = crit_chance;
            cannonSt.CriticalDamage.BaseValue = crit_damage;
            cannonSt.AttackRange.BaseValue = range;
        }
    }
}