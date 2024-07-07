using System;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(fileName = "CrewStats", menuName = "Scriptable Objects/Crew Stats", order = 1)]
    [Serializable]
    public class CrewStatsTemplate : StatsTemplate
    {
        [field: SerializeField] public CrewStats Data { get; set; } = new();

        public override void ApplyConfig(Stats stats)
        {
            CrewStats crewStats = stats as CrewStats;
            crewStats.MoveSpeed.BaseValue = Data.MoveSpeed.BaseValue;
        }
    }
}