using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ammo Stats Template")]
[Serializable]
public class AmmoStatsTemplate : StatsTemplate
{
    [field: SerializeField] public AmmoStats Data { get; set; } = new();
    public override void ApplyConfig(Stats stats)
    {
        AmmoStats projectileStats = stats as AmmoStats;
        projectileStats.HealthPoint.MaxStatValue.BaseValue = Data.HealthPoint.MaxStatValue.BaseValue;
        projectileStats.HealthPoint.StatValue.BaseValue = Data.HealthPoint.MaxStatValue.BaseValue;
        projectileStats.MagazineSize.BaseValue = Data.MagazineSize.BaseValue;
        projectileStats.EnergyCost.BaseValue = Data.EnergyCost.BaseValue;
    }
}
