
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public class SlowEffect : UnstackableEffect, IProbabilityEffect
    {
        public override string Id => "SlowEffect";
        [field: SerializeField]
        public float Prob { get; set; }

        StatModifier statModifer = new StatModifier(-.5f, StatModType.PercentMult);

        public override bool CanEffect(IEffectTaker entity)
        {
            return entity is ISlowable;
        }
        public override void Apply(IEffectTaker entity)
        {
            if (entity is not ISlowable)
                return;
            base.Apply(entity);
            Debug.Log("APLLY SLOW");
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
            if (entity is not ISlowable)
                return;
            ISlowable slowable = entity as ISlowable;
            foreach (Stat stat in slowable.SlowableStats)
            {
                stat.RemoveModifier(statModifer);
            }
            slowable.OnSlowEnded();
        }

        public void SetStrength(float percent)
        {
            statModifer.Value = percent;
        }
    }
}