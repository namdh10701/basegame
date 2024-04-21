using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [CreateAssetMenu(fileName = "CannonStats", menuName = "Scriptable Objects/Cannon Stats", order = 1)]
    [Serializable]
    public class CannonStatsTemplate : ScriptableObject
    {
        [field:SerializeField] public CannonStats Data { get; set; } = new();
    }
}