using System;
using UnityEngine;

namespace _Base.Scripts.World
{
    public abstract class PeriodicEffect: Effect
    {
        [field:SerializeField]
        public float Interval { get; set; }
        
        [field:SerializeField]
        public float Duration { get; set; }

        [field:SerializeField]
        public float RemainingTime { get; private set; }

        private void Start()
        {
            RemainingTime = Duration;
            InvokeRepeating(nameof(PeriodicApply), 0, Interval);
        }

        private void Update()
        {
            RemainingTime -= Time.deltaTime;

            if (RemainingTime <= 0)
            {
                Destroy(this);
            }
        }

        private void PeriodicApply()
        {
            Apply();
        }
    }
}