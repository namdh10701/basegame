using _Game.Scripts;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ship Stats Template")]
[Serializable]
public class ShipStatsTemplate : StatsTemplate
{
    public ShipStats Template;
    public override void ApplyConfig(Stats stats)
    {
        ShipStats shipStats = stats as ShipStats;
        shipStats.BlockChance.BaseValue = Template.BlockChance.BaseValue;
        shipStats.HealthPoint.MinStatValue.BaseValue = Template.HealthPoint.MinStatValue.BaseValue;
        shipStats.HealthPoint.MaxStatValue.BaseValue = Template.HealthPoint.MaxStatValue.BaseValue;
        shipStats.HealthPoint.StatValue.BaseValue = Template.HealthPoint.StatValue.BaseValue;
       
        shipStats.ManaRegenerationRate.BaseValue = Template.ManaRegenerationRate.BaseValue;

        shipStats.ManaPoint.MinStatValue.BaseValue = Template.ManaPoint.MinStatValue.BaseValue;
        shipStats.ManaPoint.MaxStatValue.BaseValue = Template.ManaPoint.MaxStatValue.BaseValue;
        shipStats.ManaPoint.StatValue.BaseValue = Template.ManaPoint.StatValue.BaseValue;

        shipStats.AmmoLimit.BaseValue = Template.AmmoLimit.BaseValue;
        shipStats.CannonLimit.BaseValue = Template.CannonLimit.BaseValue;
    }
}
