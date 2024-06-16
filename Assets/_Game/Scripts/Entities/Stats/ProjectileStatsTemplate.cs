using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Projectile Stats Template")]
[Serializable]
public class ProjectileStatsTemplate : ScriptableObject
{
    [field: SerializeField] public ProjectileStats Data { get; set; } = new();
    public void ApplyConfig(ProjectileStats stats)
    {
        stats.Accuracy.BaseValue = Data.Accuracy.BaseValue;
        stats.AttackAOE.BaseValue = Data.AttackAOE.BaseValue;
        stats.CritChance.BaseValue = Data.CritChance.BaseValue;
        stats.CritDamage.BaseValue = Data.CritDamage.BaseValue;
        stats.ArmorPenetrate.BaseValue = Data.ArmorPenetrate.BaseValue;
        stats.Damage.BaseValue = Data.Damage.BaseValue;
        stats.Speed.BaseValue = Data.Speed.BaseValue;
    }
}
