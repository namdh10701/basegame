using System;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(fileName = "CrewStats", menuName = "Scriptable Objects/Crew Stats", order = 1)]
    [Serializable]
    public class CrewStatsTemplate : ScriptableObject
    {
        [field: SerializeField] public CrewStats Data { get; set; } = new();

        public void ApplyConfig(CrewStats stats)
        {
            stats.MoveSpeed.BaseValue = Data.MoveSpeed.BaseValue;
        }
    }
}