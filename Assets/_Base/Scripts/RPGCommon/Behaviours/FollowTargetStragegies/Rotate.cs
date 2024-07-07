using System.Linq;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Behaviours.FollowTarget;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.FollowTargetStragegies
{
    [AddComponentMenu("RPG/FollowTargetStrategy/[FollowTargetStrategy] Rotate")]
    public class Rotate : FollowTargetStrategy
    {
        [field: SerializeField]
        public float RotateSpeed { get; set; } = 10f;

        [field: SerializeField]
        public Transform RotateTarget { get; set; }

        [field: SerializeField]
        public Vector3 Direction { get; private set; }

        [field: SerializeField]
        public Quaternion Rotation { get; private set; }

        public float changeTargetTime = 2;
        public float changeTargetTimer = 0;
        public override bool Follow(FindTargetBehaviour findTargetBehaviour)
        {
            if (findTargetBehaviour.MostTargets.Count == 0)
            {
                RotateTarget.rotation = RotateSpeed > -1
                    ? Quaternion.Slerp(RotateTarget.rotation, Quaternion.identity, 5 * Time.deltaTime)
                    : Rotation;
                return false;
            }

            if (_targetTransform == null)
            {

                _targetTransform = findTargetBehaviour.MostTargets.First().transform.GetComponent<EnemyModel>();
                changeTargetTimer = 0;
            }
            else
            {
                if (!_targetTransform.EffectTakerCollider.gameObject.activeSelf)
                {
                    _targetTransform = null;
                    return false;
                }
                if (_targetTransform.transform != findTargetBehaviour.MostTargets.First().transform)
                {
                    changeTargetTimer += Time.deltaTime;
                    if (changeTargetTimer > changeTargetTime)
                    {
                        _targetTransform = findTargetBehaviour.MostTargets.First().transform.GetComponent<EnemyModel>();
                        changeTargetTimer = 0;
                    }
                }

            }

            Direction = _targetTransform.transform.position - RotateTarget.position;
            Rotation = Quaternion.LookRotation(Vector3.forward, Direction);

            RotateTarget.rotation = RotateSpeed > -1
                ? Quaternion.Slerp(RotateTarget.rotation, Rotation, RotateSpeed * Time.deltaTime)
                : Rotation;

            var angle = Quaternion.Angle(RotateTarget.transform.rotation, Rotation);

            return angle < 2f;
        }


        public EnemyModel _targetTransform;
        private void OnDrawGizmos()
        {
            if (_targetTransform != null)
            {
                Gizmos.DrawRay(RotateTarget.position, Direction);
            }

        }
    }
}