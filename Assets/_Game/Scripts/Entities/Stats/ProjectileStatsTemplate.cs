using _Game.Scripts;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Projectile Stats Template")]
[Serializable]
public class ProjectileStatsTemplate : StatsTemplate
{
    [field: SerializeField] public ProjectileStats Data { get; set; } = new();
    public override void ApplyConfig(Stats stats)
    {
        ProjectileStats projectileStats = stats as ProjectileStats;
        projectileStats.Accuracy.BaseValue = Data.Accuracy.BaseValue;
        projectileStats.AttackAOE.BaseValue = Data.AttackAOE.BaseValue;
        projectileStats.CritChance.BaseValue = Data.CritChance.BaseValue;
        projectileStats.CritDamage.BaseValue = Data.CritDamage.BaseValue;
        projectileStats.ArmorPenetrate.BaseValue = Data.ArmorPenetrate.BaseValue;
        projectileStats.Damage.BaseValue = Data.Damage.BaseValue;
        projectileStats.Speed.BaseValue = Data.Speed.BaseValue;
    }
}
