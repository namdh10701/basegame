using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Attributes;
using UnityEngine;

namespace _Game.Scripts.Effects
{
    public class IncreaseHealthPointEffect: OneShotEffect
    {
        [field:SerializeField]
        public int Amount { get; set; }

        private HealthPoint _hp;

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
            _hp.BaseValue += Amount;
        }
    }
}