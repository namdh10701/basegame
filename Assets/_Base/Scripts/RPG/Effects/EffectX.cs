using System;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace _Base.Scripts.RPG.Effects
{
    [Serializable]
    public abstract class EffectX
    {
        // public List<StatModifier> StatModifiers;

        public FindTargetBehaviour FindTargetBehaviour;

        public abstract void Apply(Entity entity);

        public bool IsDone { get; protected set; }
        

        protected virtual void OnStart(Entity entity) {}
        protected virtual void OnEnd(Entity entity) {}

        public virtual bool CanEffect(Entity entity) => true;
    }

    public abstract class OneShotEffectX: EffectX
    {
        public override void Apply(Entity entity)
        {
            OnStart(entity);
            OnApply(entity);
            OnEnd(entity);
            IsDone = true;
        }

        protected abstract void OnApply(Entity entity);
    }

    // public abstract class PermanentEffectX : EffectX
    // {
    //
    // }

    public abstract class TimeoutEffectX: EffectX
    {
        protected TimeoutEffectX(int duration)
        {
            Duration = duration;
        }

        public int Duration { get; set; }
        
        public override async void Apply(Entity entity)
        {
            OnStart(entity);
            await Task.Delay(Duration);
            OnEnd(entity);
            IsDone = true;
        }
    }

    public abstract class PeriodicEffectX: TimeoutEffectX
    {
        protected PeriodicEffectX(int interval, int duration): base(duration)
        {
            Interval = interval;
        }

        public int Interval { get; set; }
        
        public override async void Apply(Entity entity)
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

        protected abstract void OnTick(Entity entity);
    }

    public class DecreaseHealthEffect: OneShotEffectX
    {
        public float Amount { get; set; }
        
        public DecreaseHealthEffect(float amount)
        {
            Amount = amount;
        }

        protected override void OnApply(Entity entity)
        {
            if (entity.Stats is not IAlive alive)
            {
                return;
            }

            alive.HealthPoint -= Amount;
        }

    }
    
    public class IncreaseHealthEffect: OneShotEffectX
    {
        public float Amount { get; set; }
        
        public IncreaseHealthEffect(float amount)
        {
            Amount = amount;
        }

        protected override void OnApply(Entity entity)
        {
            if (entity.Stats is not IAlive alive)
            {
                return;
            }

            alive.HealthPoint += Amount;
        }

    }
    
    public class DrainHealthEffect: PeriodicEffectX
    {
        public float Amount { get; set; }

        public DrainHealthEffect(float amount, int interval, int duration): base(interval, duration)
        {
            Amount = amount;
        }

        protected override void OnTick(Entity entity)
        {
            if (entity.Stats is not IAlive alive)
            {
                return;
            }
            alive.HealthPoint -= Amount;
        }
    }
    
    public class AddPermanantMaxHealthEffect: OneShotEffectX
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
    
    public class AddTempMaxHealthEffect: TimeoutEffectX
    {
        public float Amount { get; set; }


        protected override void OnStart(Entity entity)
        {
            base.OnStart(entity);
            if (entity.Stats is not IAlive alive)
            {
                return;
            }
            
            alive.MaxHealthPoint.AddModifier(StatModifier.Flat(Amount));
            alive.HealthPoint += Amount;
        }
        
        protected override void OnEnd(Entity entity)
        {
            base.OnStart(entity);
            if (entity.Stats is not IAlive alive)
            {
                return;
            }

            alive.MaxHealthPoint.RemoveAllModifiersFromSource(this);
        }

        public AddTempMaxHealthEffect(int duration) : base(duration)
        {
        }
    }
}