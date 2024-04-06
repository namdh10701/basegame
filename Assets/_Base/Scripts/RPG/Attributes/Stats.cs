using System;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Attributes
{

    [Serializable]
    public abstract class Stats
    {
        
    }

    [Serializable]
    public class CannonStats : Stats, IAlive, IFighter
    {


        [field: SerializeField]
        public Stat MaxHealthPoint { get; set; } = new();

        [field: SerializeField]
        public Stat AttackDamage {get;set;} = new();
        
        [field: SerializeField]
        public Stat CriticalChance {get;set;} = new();
        
        [field: SerializeField]
        public Stat CriticalDamage {get;set;} = new();

        [field: SerializeField]
        public Stat AttackRange { get; set; } = new();
        
        public IFighter.AttackTypes AttackType { get; set; } = IFighter.AttackTypes.UNIT;

        [SerializeField]
        private float _healthPoint;
        float IAlive.HealthPoint
        {
            get => _healthPoint;
            set => _healthPoint = value;
        }
    }
}