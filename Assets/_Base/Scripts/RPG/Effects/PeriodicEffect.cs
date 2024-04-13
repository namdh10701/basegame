using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    /// <summary>
    /// Apply effect periodically in amount of time
    /// </summary>
    public abstract class PeriodicEffect: TimeoutEffect
    {
        [field:SerializeField]
        public float Interval { get; set; }

        protected override void StartOverride()
        {
            base.StartOverride();
            InvokeRepeating(nameof(PeriodicApply), Interval, Interval);
        }

        private void PeriodicApply()
        {
            Apply();
        }
    }
}