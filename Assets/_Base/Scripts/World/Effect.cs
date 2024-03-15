using System;
using UnityEngine;

namespace _Base.Scripts.World
{
    public abstract class Effect: MonoBehaviour
    {
        [field:SerializeField]
        public bool IsInbound { get; set; }
        
        [field:SerializeField]
        public bool IsOutbound { get; set; }

        protected abstract void Apply();

        // public Attribute[] GetEffectTarget()
        // {
        //     
        // }

        private void OnDestroy()
        {
            Debug.Log($"[{GetType().Name}] Destroyed");
        }
    }
}