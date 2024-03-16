using _Base.Scripts.RPG;
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
            _hp = GetComponent<Entity>()?.GetAttribute<HealthPoint>();
        }

        public override void Apply()
        {
            if (_hp == null)
            {
                return;
            }
            _hp.Value += Amount;
        }
    }
}