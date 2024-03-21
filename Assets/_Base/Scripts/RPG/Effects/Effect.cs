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

        private void OnDestroy()
        {
            Debug.Log($"[{GetType().Name}] Destroyed");
        }

        public event EventHandler<EffectEventArgs> OnStart;
        public event EventHandler<EffectEventArgs> OnEnd;
        public abstract void Apply();
        public void OnBeforeApply()
        {
            NotifyStarted();
        }

        public void OnAfterApply()
        {
            NotifyEnded();
        }

        public void NotifyStarted()
        {
            OnStart?.Invoke(this, new EffectEventArgs());
        }
        
        public void NotifyEnded()
        {
            OnEnd?.Invoke(this, new EffectEventArgs());
        }
    }
}