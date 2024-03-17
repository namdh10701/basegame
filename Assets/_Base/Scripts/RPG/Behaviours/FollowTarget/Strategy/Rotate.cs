using _Base.Scripts.RPG.Behaviours.FindTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FollowTarget.Strategy
{
    [AddComponentMenu("RPG/FollowTargetStrategy/[FollowTargetStrategy] Rotate")]
    public class Rotate: FollowTargetStrategy
    {
        [field: SerializeField] 
        public float RotateSpeed { get; set; } = 10f;
        
        [field:SerializeField]
        public Transform RotateTarget { get; set; }
        public override bool Follow(FindTargetBehaviour findTargetBehaviour)
        {
            if (!findTargetBehaviour.MostTarget)
            {
                return false;
            }
            
            var followTarget = findTargetBehaviour.MostTarget.transform;
            
            Vector3 targetDirection = followTarget.position - RotateTarget.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

            RotateTarget.rotation = RotateSpeed > -1 
                ? Quaternion.Slerp(RotateTarget.rotation, targetRotation, RotateSpeed * Time.deltaTime) 
                : targetRotation;

            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            return angle < 2f;
        }
    }
}