using System;
using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    public abstract class Effect: MonoBehaviour, IEffect
    {
        [field:SerializeField]
        public bool IsInbound { get; set; }
        
        [field:SerializeField]
        public bool IsOutbound { get; set; }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Debug.Log($"[{GetType().Name}] Destroyed");
        }

        public event EventHandler<EffectEventArgs> OnStart;
        public event EventHandler<EffectEventArgs> OnEnd;
        public abstract void Apply();
        public abstract void Process();

        public virtual void OnBeforeApply()
        {
            NotifyStarted();
        }

        public virtual void OnAfterApply()
        {
            NotifyEnded();
        }

        public virtual void NotifyStarted()
        {
            OnStart?.Invoke(this, new EffectEventArgs());
        }
        
        public virtual void NotifyEnded()
        {
            OnEnd?.Invoke(this, new EffectEventArgs());
        }
    }
}