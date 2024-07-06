using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;

namespace _Base.Scripts.RPG.Effects
{
    public class AddTempMaxHealthEffect: TimeoutEffect
    {
        public float Amount { get; set; }


        protected override void OnStart(IEffectTaker entity)
        {
            base.OnStart(entity);
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }
            
            alive.HealthPoint.StatValue.AddModifier(StatModifier.Flat(Amount));
            // alive.HealthPoint += Amount;
        }
        
        protected override void OnEnd(IEffectTaker entity)
        {
            base.OnStart(entity);
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }

            alive.HealthPoint.StatValue.RemoveAllModifiersFromSource(this);
        }

        public AddTempMaxHealthEffect(int duration) : base(duration)
        {
        }
    }
}