using _Base.Scripts.RPG;
using _Game.Scripts.Attributes;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game.Scripts.Effects
{
    public class DrainHealthPointEffect: PeriodicEffect
    {
        [field:SerializeField]
        public int Amount { get; set; }
        
        [CanBeNull] private HealthPoint _hp;

        private void Awake()
        {
            _hp = GetComponentInParent<Entity>()?.GetAttribute<HealthPoint>();
        }

        public override void Apply()
        {
            if (_hp == null)
            {
                return;
            }
            _hp.Value -= Amount;
        }
    }
}