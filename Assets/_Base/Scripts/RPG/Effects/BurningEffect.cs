using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;

namespace _Base.Scripts.RPG.Effects
{
    public class BurningEffect : UnstackablePeriodicEffect
    {
        public float Amount { get; set; }

        public override string Id => nameof(BurningEffect);

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