using System.Linq;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Behaviours.FollowTarget;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.FollowTargetStragegies
{
    [AddComponentMenu("RPG/FollowTargetStrategy/[FollowTargetStrategy] Rotate")]
    public class Rotate: FollowTargetStrategy
    {
        [field: SerializeField] 
        public float RotateSpeed { get; set; } = 10f;
        
        [field:SerializeField]
        public Transform RotateTarget { get; set; }
        
        [field:SerializeField]
        public Vector3 Direction { get; private set; }
        
        [field:SerializeField]
        public Quaternion Rotation { get; private set; }
        
        public override bool Follow(FindTargetBehaviour findTargetBehaviour)
        {
            if (findTargetBehaviour.MostTargets.Count == 0)
            {
                return false;
            }
            
            var targetTransform = findTargetBehaviour.MostTargets.First().transform;
            _targetTransform = targetTransform;
            Direction = targetTransform.position - RotateTarget.position;
            Rotation = Quaternion.LookRotation(Vector3.forward, Direction);

            RotateTarget.rotation = RotateSpeed > -1 
                ? Quaternion.Slerp(RotateTarget.rotation, Rotation, RotateSpeed * Time.deltaTime) 
                : Rotation;

            var angle = Quaternion.Angle(RotateTarget.transform.rotation, Rotation);
            
            return angle < 2f;
        }

        private Transform _targetTransform;
        private void OnDrawGizmos()
        {
            if (_targetTransform != null)
            {
                Gizmos.DrawRay(RotateTarget.position, Direction);
            }

        }
    }
}