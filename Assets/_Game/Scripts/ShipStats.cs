using System;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class ShipStats : Stats//, IAlive
    {
        [field: SerializeField]
        public RangedValue HealthPoint { get; set; } = new(0, 0, 100);
        
        [field: SerializeField]
        public RangedValue ManaPoint { get; set; } = new(0, 0, 150);
        
        [field: SerializeField]
        public Stat ManaRegenerationRate { get; set; } = new(5, 0, 10);
        
        [field: SerializeField]
        public Stat HealthRegenerationRate { get; set; } = new(1, 0, 10);
    }
}