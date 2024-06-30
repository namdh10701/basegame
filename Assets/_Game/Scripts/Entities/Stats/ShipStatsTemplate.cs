using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ship Stats Template")]
[Serializable]
public class ShipStatsTemplate : ScriptableObject
{
    [field: SerializeField] public ShipStats Data { get; set; } = new();

    public void ApplyConfig(ShipStats shipStats)
    {
        shipStats.BlockChance.BaseValue = Data.BlockChance.BaseValue;
        shipStats.HealthPoint.MinStatValue.BaseValue = Data.HealthPoint.MinStatValue.BaseValue;
        shipStats.HealthPoint.MaxStatValue.BaseValue = Data.HealthPoint.MaxStatValue.BaseValue;
        shipStats.HealthPoint.StatValue.BaseValue = Data.HealthPoint.StatValue.BaseValue;
        shipStats.HealthRegenerationRate.MinStatValue.BaseValue = Data.HealthPoint.MinStatValue.BaseValue;
        shipStats.HealthRegenerationRate.MaxStatValue.BaseValue = Data.HealthPoint.MaxStatValue.BaseValue;
        shipStats.HealthRegenerationRate.StatValue.BaseValue = Data.HealthPoint.StatValue.BaseValue;

        shipStats.ManaRegenerationRate.BaseValue = Data.ManaRegenerationRate.BaseValue;

        shipStats.ManaPoint.MinStatValue.BaseValue = Data.ManaPoint.MinStatValue.BaseValue;
        shipStats.ManaPoint.MaxStatValue.BaseValue = Data.ManaPoint.MaxStatValue.BaseValue;
        shipStats.ManaPoint.StatValue.BaseValue = Data.ManaPoint.StatValue.BaseValue;

        shipStats.AmmoLimit.BaseValue = Data.AmmoLimit.BaseValue;
        shipStats.CannonLimit.BaseValue = Data.CannonLimit.BaseValue;
    }
}
