using System;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Task = System.Threading.Tasks.Task;

namespace _Base.Scripts.RPG.Effects
{

    public interface IDamageEffect
    {
        public float Amount { get; set; }
        public float ArmorPenetrate { get; set; }
    }

    [Serializable]
    public abstract class Effect : MonoBehaviour
    {
        public IEffectGiver Giver;
        public IEffectTaker Target;
        public virtual void Apply(IEffectTaker entity)
        {
            IsActive = true;
            Target = entity;
        }
        [field: SerializeField]
        public bool IsDone { get; protected set; }
        public bool IsActive { get; protected set; }

        public Action<Effect> OnEnded;

        protected virtual void OnStart(IEffectTaker entity) { }
        public virtual void OnEnd(IEffectTaker entity)
        {
            OnEnded?.Invoke(this);
            IsActive = false;
            IsDone = true;
        }
        public virtual bool CanEffect(IEffectTaker entity) => true;
    }

    public abstract class UnstackableEffect : TimeoutEffect
    {
        public abstract string Id { get; }
        public virtual void RefreshEffect(UnstackableEffect newEffect)
        {
            Duration = Mathf.Max(newEffect.Duration, elapsedTime);
            elapsedTime = 0;
        }
        public override void OnEnd(IEffectTaker entity)
        {
            base.OnEnd(entity);

            Destroy(gameObject);
        }
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
        public float Duration { get; set; }
        [field: SerializeField]
        protected float elapsedTime = 0;
        public IEffectTaker Affected { get; protected set; }
        protected override void OnStart(IEffectTaker entity)
        {
            base.OnStart(entity);
            IsActive = true;
            transform.parent = null;
        }
        public override void OnEnd(IEffectTaker entity)
        {
            base.OnEnd(entity);
        }
        public override void Apply(IEffectTaker entity)
        {
            base.Apply(entity);
            OnStart(entity);
        }
        public void SetDuration(float duration)
        {
            this.Duration = duration;
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
    public abstract class UnstackablePeriodicEffect : UnstackableEffect
    {
        [field: SerializeField]
        public float Interval { get; set; }
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
                    intervalElapsedTime = 0;
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