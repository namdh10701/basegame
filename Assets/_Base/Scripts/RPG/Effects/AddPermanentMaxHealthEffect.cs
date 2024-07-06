using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;

namespace _Base.Scripts.RPG.Effects
{
    public class AddPermanentMaxHealthEffect: OneShotEffect
    {
        public float Amount { get; set; }


        protected override void OnApply(IEffectTaker entity)
        {
            if (entity.Stats is not IAliveStats alive)
            {
                return;
            }
            
            alive.HealthPoint.StatValue.AddModifier(StatModifier.Flat(Amount));
        }
    }
}