using System;
using _Base.Scripts.RPG.Entities;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public abstract class Effect : MonoBehaviour
    {
        public IEffectTaker Target;
        public virtual void Apply(IEffectTaker entity)
        {
            Target = entity;
            IsActive = true;
            transform.parent = null;
        }
        [field: SerializeField]
        public bool IsActive { get; protected set; }
        public bool IsDone { get; protected set; }

        public Action<Effect> OnEnded;

        protected virtual void OnStart(IEffectTaker entity) { }
        public virtual void OnEnd(IEffectTaker entity)
        {
            OnEnded?.Invoke(this);
            Destroy(gameObject);
        }

        public virtual bool CanEffect(Entity entity) => true;
        public virtual bool CanEffect(IEffectTaker entity) => true;
    }


    public abstract class OneShotEffect : Effect
    {
        public override void Apply(IEffectTaker entity)
        {
            base.Apply(entity);
            OnStart(entity);
            OnApply(entity);
            OnEnd(entity);
            IsDone = true;
        }

        protected abstract void OnApply(IEffectTaker entity);
    }


    public abstract class TimeoutEffect : Effect
    {
        [field: SerializeField]
        public int Duration { get; set; }
        [field: SerializeField]
        protected float elapsedTime = 0;

        public override void Apply(IEffectTaker entity)
        {
            base.Apply(entity);
            OnStart(entity);
        }

        private void Update()
        {
            if (IsActive)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > Duration)
                {
                    OnEnd(Target);
                    IsDone = true;
                }
            }
        }
    }

    public abstract class PeriodicEffect : TimeoutEffect
    {

        [field: SerializeField]
        public int Interval { get; set; }
        public float intervalElapsedTime = 0;
        public override void Apply(IEffectTaker entity)
        {
            base.Apply(entity);
            OnStart(entity);
        }

        private void Update()
        {
            if (IsActive)
            {
                elapsedTime += Time.deltaTime;
                intervalElapsedTime += Time.deltaTime;
                if (intervalElapsedTime > Interval)
                {
                    OnTick(Target);
                }
                if (elapsedTime > Duration)
                {
                    OnEnd(Target);
                    IsDone = true;
                }
            }
        }
        protected abstract void OnTick(IEffectTaker entity);
    }
}