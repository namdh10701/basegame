using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    /// <summary>
    /// Apply effect periodically in amount of time
    /// </summary>
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
            OnBeforeApply();
            RemainingTime = Duration;
            InvokeRepeating(nameof(PeriodicApply), 0, Interval);
        }

        private void Update()
        {
            RemainingTime -= Time.deltaTime;

            if (!(RemainingTime <= 0)) return;
            
            OnAfterApply();
            Destroy(this);
        }

        private void PeriodicApply()
        {
            Apply();
        }
    }
}