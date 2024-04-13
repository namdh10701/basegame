using UnityEngine;

namespace _Base.Scripts.RPG.Effects
{
    /// <summary>
    /// Apply effect periodically in amount of time
    /// </summary>
    public abstract class TimeoutEffect: Effect
    {
        [field:SerializeField]
        public float Duration { get; set; }

        [field:SerializeField]
        public float RemainingTime { get; private set; }

        public override void Process()
        {
            StartOverride();
        }

        // private void Start()
        // {
        //     StartOverride();
        // }

        protected virtual void StartOverride()
        {
            OnBeforeApply();
            RemainingTime = Duration;
            Apply();
        }

        private void Update()
        {
            RemainingTime -= Time.deltaTime;

            if (!(RemainingTime <= 0)) return;
            
            OnAfterApply();
            Destroy(this);
        }
    }
}