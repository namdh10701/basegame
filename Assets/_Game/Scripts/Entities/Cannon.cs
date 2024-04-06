using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Attributes;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Cannon: Entity, IShooter
    {
        public Vector3 AimDirection => transform.rotation.eulerAngles;

        [SerializeField]
        private CannonStats _stats = new CannonStats();
        public override Stats Stats => _stats;

        private void Awake()
        {
            // Attributes.Add(new HealthPoint());
            // Attributes.Add(new ManaPoint());
        }

        // public HealthPoint HealthPoint { get; set; }
        // public AttackDamage AttackDamage { get; set; }
        // public CriticalChance CriticalChance { get; set; }
        // public CriticalDamage CriticalDamage { get; set; }
    }
}