using System;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(fileName = "CellStats", menuName = "Scriptable Objects/Cell Stats", order = 1)]
    [Serializable]
    public class CellStatsTemplate : ScriptableObject
    {
        [field: SerializeField] public CellStats Data { get; set; } = new();

        public void ApplyConfig(CellStats stats)
        {
            stats.HealthPoint.StatValue.BaseValue = Data.HealthPoint.StatValue.BaseValue;
            stats.HealthPoint.MinStatValue.BaseValue = Data.HealthPoint.MinStatValue.BaseValue;
            stats.HealthPoint.MaxStatValue.BaseValue = Data.HealthPoint.MaxStatValue.BaseValue;
        }
    }
}