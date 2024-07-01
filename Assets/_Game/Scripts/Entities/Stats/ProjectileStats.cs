using _Base.Scripts.RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    [Serializable]
    public class ProjectileStats : Stats
    {
        [field: SerializeField]
        public Stat Damage { get; set; } = new();

        [field: SerializeField]
        public Stat AttackAOE { get; set; } = new();

        [field: SerializeField]
        public Stat ArmorPenetrate { get; set; } = new();

        [field: SerializeField]
        public Stat Piercing { get; set; } = new();

        [field: SerializeField]
        public Stat Speed { get; set; } = new();

        [field: SerializeField]
        public Stat Size { get; set; } = new();
        [field: SerializeField]
        public Stat Accuracy { get; set; } = new();

        [field: SerializeField]
        public Stat CritChance { get; set; } = new();

        [field: SerializeField]
        public Stat CritDamage { get; set; } = new();

        [field: SerializeField]
        public Stat MagazineSize { get; set; } = new();

        [field: SerializeField]
        public Stat EnergyCost { get; set; } = new();
    }
}
