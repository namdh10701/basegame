using System;
using _Base.Scripts.RPG.Entities;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
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
        }
        [field: SerializeField]
        public bool IsDone { get; protected set; }

        public Action<Effect> OnEnded;

        protected virtual void OnStart(IEffectTaker entity) { }
        public virtual void OnEnd(IEffectTaker entity)
        {
            OnEnded?.Invoke(this);
            Destroy(gameObject);
        }
        public virtual bool CanEffect(IEffectTaker entity) => true;
    }

    public abstract class UnstackableEffect : TimeoutEffect
    {
        public abstract string Id { get; }
        public IEffectTaker Affected { get; protected set; }
        protected override void OnStart(IEffectTaker entity)
        {
            base.OnStart(entity);
            transform.parent = null;
        }

        public override void Apply(IEffectTaker entity)
        {
            base.Apply(entity);
            if (TryGetEffect(this, out UnstackableEffect existEffect))
            {
                RefreshEffect(existEffect);
            }
        }
        bool TryGetEffect(UnstackableEffect findEffect, out UnstackableEffect existEffect)
        {
            foreach (Effect effect in Target.EffectHandler.effects.ToArray())
            {
                if (effect is UnstackableEffect unstackableEffect && findEffect.Id == this.Id)
                {
                    existEffect = unstackableEffect;
                    return true;
                }
            }
            existEffect = null;
            return false;
        }
        public virtual void RefreshEffect(UnstackableEffect existEffect)
        {
            existEffect.Duration = this.Duration;
            elapsedTime = 0;
        }
        public override void OnEnd(IEffectTaker entity)
        {
            base.OnEnd(entity);
            if (Affected != entity)
                return;
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
        public int Duration { get; set; }
        public bool IsActive { get; protected set; }
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