using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;

namespace _Base.Scripts.RPG.Effects
{
    public class AddPermanentMaxHealthEffect: OneShotEffect
    {
        public float Amount { get; set; }


        protected override void OnApply(Entity entity)
        {
            if (entity.Stats is not IAlive alive)
            {
                return;
            }
            
            alive.MaxHealthPoint.AddModifier(StatModifier.Flat(Amount));
        }
    }
}