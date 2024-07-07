using System;
using _Base.Scripts.RPG.Entities;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public abstract class Effect : MonoBehaviour
    {
        public abstract void Apply(IEffectTaker entity);

        public bool IsDone { get; protected set; }


        protected virtual void OnStart(IEffectTaker entity) { }
        protected virtual void OnEnd(IEffectTaker entity)
        {
            if (gameObject.name == "")
            {
                Destroy(gameObject);
            }
        }

        public virtual bool CanEffect(Entity entity) => true;
        public virtual bool CanEffect(IEffectTaker entity) => true;
    }


    public abstract class OneShotEffect : Effect
    {
        public override void Apply(IEffectTaker entity)
        {
            OnStart(entity);
            OnApply(entity);
            OnEnd(entity);
            IsDone = true;
        }

        protected abstract void OnApply(IEffectTaker entity);
    }


    public abstract class TimeoutEffect : Effect
    {
        protected TimeoutEffect(int duration)
        {
            Duration = duration;
        }

        [field: SerializeField]
        public int Duration { get; set; }

        public override async void Apply(IEffectTaker entity)
        {
            OnStart(entity);
            await Task.Delay(Duration);
            OnEnd(entity);
            IsDone = true;
        }
    }

    public abstract class PeriodicEffect : TimeoutEffect
    {
        protected PeriodicEffect(int interval, int duration) : base(duration)
        {
            Interval = interval;
        }

        [field: SerializeField]
        public int Interval { get; set; }

        public override async void Apply(IEffectTaker entity)
        {
            var startTime = Time.time;
            OnStart(entity);
            do
            {
                OnTick(entity);
                await Task.Delay(Interval * 1000);
            } while (Time.time - startTime < Duration);
            OnEnd(entity);
            IsDone = true;
        }

        protected abstract void OnTick(IEffectTaker entity);
    }
}