using System;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public class DecreaseHealthEffect: OneShotEffect
    {
        public float Amount { get; set; }
        
        public DecreaseHealthEffect(float amount)
        {
            Amount = amount;
        }

        protected override void OnApply(Entity entity)
        {
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }

            alive.HealthPoint.StatValue.BaseValue -= Amount;
        }

    }
}