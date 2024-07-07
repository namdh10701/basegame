
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public class SlowEffect : TimeoutEffect
    {
        public string id = "slowFromCannon";
        StatModifier statModifer = new StatModifier(-.5f, StatModType.PercentMult);
        ISlowable affected;

        protected override void OnStart(IEffectTaker entity)
        {
            base.OnStart(entity);
            if (entity is ISlowable slowable)
            {
                slowable.OnSlowed();
                affected = slowable;
                foreach (Stat stat in slowable.SlowableStats)
                {
                    stat.AddModifier(statModifer);
                }
            }
        }

        public override void OnEnd(IEffectTaker entity)
        {
            base.OnEnd(entity);
            if (affected != null)
            {
                affected.OnSlowEnded();
                foreach (Stat stat in affected.SlowableStats)
                {
                    stat.RemoveModifier(statModifer);
                }
            }
        }

        private void OnDestroy()
        {
            Debug.Log("DESTROYED");
        }

    }
}