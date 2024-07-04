
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;

namespace _Base.Scripts.RPG.Effects
{
    public class SlowEffect : PeriodicEffect
    {
        StatModifier statModifer;
        ISlowable affected;

        public SlowEffect(float strength, int interval, int duration) : base(interval, duration)
        {
            statModifer = new StatModifier(strength, StatModType.PercentAdd);
        }

        protected override void OnStart(Entity entity)
        {
            if (entity is ISlowable slowable)
            {
                affected = slowable;
                foreach (Stat stat in slowable.SlowableStats)
                {
                    stat.AddModifier(statModifer);
                }
            }
        }

        protected override void OnTick(Entity entity)
        {

        }
        protected override void OnEnd(Entity entity)
        {
            if (affected != null)
            {
                foreach (Stat stat in affected.SlowableStats)
                {
                    stat.RemoveModifier(statModifer);
                }
            }
        }

    }
}