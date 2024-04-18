using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;

namespace _Base.Scripts.RPG.Effects
{
    public class IncreaseHealthEffect: OneShotEffect
    {
        public float Amount { get; set; }
        
        public IncreaseHealthEffect(float amount)
        {
            Amount = amount;
        }

        protected override void OnApply(Entity entity)
        {
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }

            alive.HealthPoint += Amount;
        }

    }
}