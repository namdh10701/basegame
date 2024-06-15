using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
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
        }
    }
}