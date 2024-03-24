using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Scripts.Attributes;
using UnityEngine;

namespace _Game.Scripts.Effects
{
    public class TempIncreaseHealthPointEffect: TimeoutEffect
    {
        [field:SerializeField]
        public int Amount { get; set; }

        private HealthPoint _hp;
        private AttributeModifier<int> _value;


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
            _value = new AttributeModifier<int>(Amount);
            _hp.Modifiers.Add(_value);
        }

        public override void OnAfterApply()
        {
            _hp.Modifiers.Remove(_value);
        }
    }
}