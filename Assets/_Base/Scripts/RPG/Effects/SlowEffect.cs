
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public class SlowEffect : UnstackableEffect
    {
        public override string Id => "SlowEffect";
        StatModifier statModifer = new StatModifier(-.5f, StatModType.PercentMult);

        public override bool CanEffect(IEffectTaker entity)
        {
            return entity is ISlowable;
        }
        public override void Apply(IEffectTaker entity)
        {
            base.Apply(entity);
            ISlowable slowable = entity as ISlowable;
            Affected = (IEffectTaker)slowable;
            slowable.OnSlowed();
            foreach (Stat stat in slowable.SlowableStats)
            {
                stat.AddModifier(statModifer);
            }
        }

        public override void OnEnd(IEffectTaker entity)
        {
            base.OnEnd(entity);
            ISlowable slowable = entity as ISlowable;
            foreach (Stat stat in slowable.SlowableStats)
            {
                stat.RemoveModifier(statModifer);
            }
            slowable.OnSlowEnded();
        }
    }
}