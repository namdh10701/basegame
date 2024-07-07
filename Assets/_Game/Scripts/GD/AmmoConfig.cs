using System;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Ammo_", menuName = "SO/Ammo", order = 1)]
    [Serializable]
    public class AmmoConfig : GDConfig, IOperationConfig
    {
        public string id;
        public string operation_type;
        public string rarity;
        public string rarity_level;
        public string name;
        public string default_rarity;
        public float hp;
        public float energy_cost;
        public float magazine_size;
        public float ammo_attack;
        public float attack_aoe;
        public float armor_pen;
        public float project_piercing;
        public float project_speed;
        public float ammo_accuracy;
        public float ammo_crit_chance;
        public float ammo_crit_damage;
        public float trigger_prob;
        public float duration;
        public float speed_modifer;
        public float dps;
        public float pierc_count;
        public float hp_threshold;

        public string OperationType { get => operation_type; set => operation_type = value; }

        public override string GetId() => id;
        public override void ApplyGDConfig(object stats)
        {
            if (stats is AmmoStats)
            {
                var cannonSt = (stats as AmmoStats);
                cannonSt.HealthPoint.MaxStatValue.BaseValue = hp;
                cannonSt.HealthPoint.StatValue.BaseValue = hp;
                cannonSt.EnergyCost.BaseValue = energy_cost;
                cannonSt.MagazineSize.BaseValue = magazine_size;
            }
            else
            {
                var cannonSt = (stats as ProjectileStats);
                cannonSt.Damage.BaseValue = ammo_attack;
                cannonSt.CritDamage.BaseValue = ammo_crit_damage;
                cannonSt.CritChance.BaseValue = ammo_crit_chance;
                cannonSt.Accuracy.BaseValue = ammo_accuracy;
                cannonSt.ArmorPenetrate.BaseValue = armor_pen;
                cannonSt.AttackAOE.BaseValue = attack_aoe;
                cannonSt.Speed.BaseValue = project_speed;
                cannonSt.ArmorPenetrate.BaseValue = armor_pen;
                cannonSt.Piercing.BaseValue = project_piercing;
            }
        }
    }
}