using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public abstract class Effect: MonoBehaviour, IEffect
    {
        [field:SerializeField]
        public bool IsInbound { get; set; }
        
        [field:SerializeField]
        public bool IsOutbound { get; set; }

        // public Attribute[] GetEffectTarget()
        // {
        //     
        // }

        private void OnDestroy()
        {
            Debug.Log($"[{GetType().Name}] Destroyed");
        }

        public event EventHandler<EffectEventArgs> OnStarted;
        public event EventHandler<EffectEventArgs> OnEnd;
        public abstract void Apply();
        public void DoStart()
        {
            NotifyStarted();
        }

        public void DoEnd()
        {
            NotifyEnded();
        }

        public void NotifyStarted()
        {
            OnStarted?.Invoke(this, new EffectEventArgs());
        }
        
        public void NotifyEnded()
        {
            OnEnd?.Invoke(this, new EffectEventArgs());
        }
    }
}