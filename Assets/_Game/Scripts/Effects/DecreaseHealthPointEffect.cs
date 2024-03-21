using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Attributes;
using JetBrains.Annotations;
using UnityEngine;

namespace _Game.Scripts.Effects
{
    public class DecreaseHealthPointEffect: OneShotEffect
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