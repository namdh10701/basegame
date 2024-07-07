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

        protected override void OnTick(IEffectTaker entity)
        {
            if (entity is not IStatsBearer statsBearer)
            {
                return;
            }
            if (statsBearer is not IAliveStats alive)
            {
                return;
            }
            alive.HealthPoint.StatValue.BaseValue -= Amount;
        }
    }
}