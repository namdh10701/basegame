using _Base.Scripts.RPG.Behaviours.FollowTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.AimTarget.Strategy
{
    [AddComponentMenu("RPG/AimTargetStrategy/[AimTargetStrategy] TimeBasedAiming")]
    public class TimeBased: AimTargetStrategy
    {
        [field: SerializeField] 
        public float AimingTime { get; set; } = 3f;

        private float _totalAimingTime = 0;
        
        public override bool Aim(FollowTargetBehaviour followTargetBehaviour)
        {
            _totalAimingTime += Time.deltaTime;

            return _totalAimingTime >= AimingTime;
        }

        public override void Reset()
        {
            _totalAimingTime = 0;
        }
    }
}