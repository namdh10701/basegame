using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public class IncreaseHealthEffect: OneShotEffect
    {
        [field:SerializeField]
        public float Amount { get; set; }
        
        public IncreaseHealthEffect(float amount)
        {
            Amount = amount;
        }

        protected override void OnApply(IEffectTaker entity)
        {
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }

            alive.HealthPoint.StatValue.BaseValue += Amount;
        }

    }
}