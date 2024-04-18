using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;

namespace _Base.Scripts.RPG.Effects
{
    public class DrainHealthEffect: PeriodicEffect
    {
        public float Amount { get; set; }

        public DrainHealthEffect(float amount, int interval, int duration): base(interval, duration)
        {
            Amount = amount;
        }

        protected override void OnTick(Entity entity)
        {
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }
            alive.HealthPoint -= Amount;
        }
    }
}