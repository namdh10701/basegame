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



            var cannonSt = (stats as ProjectileStats)!;
            cannonSt.Damage.BaseValue = ammo_attack;
            cannonSt.CritDamage.BaseValue = ammo_crit_damage;
            cannonSt.CritChance.BaseValue = ammo_crit_chance;
            cannonSt.Accuracy.BaseValue = ammo_accuracy;
            cannonSt.ArmorPenetrate.BaseValue = armor_pen;
            cannonSt.AttackAOE.BaseValue = attack_aoe;
            cannonSt.Speed.BaseValue = 20;
            cannonSt.ArmorPenetrate.BaseValue = armor_pen;
            cannonSt.Piercing.BaseValue = pierc_count;
            cannonSt.MagazineSize.BaseValue = magazine_size;
        }
    }
}