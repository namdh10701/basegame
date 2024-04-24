using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Behaviours.FollowTarget;
using System.Linq;
using UnityEngine;
namespace _Base.Scripts.RPGCommon.Behaviours.FollowTargetStragegies
{
    public class MoveClose : FollowTargetStrategy
    {
        public MoveBehaviour MoveBehaviour;
        ObjectCollisionDetector objectCollisionDetector;
        Transform _targetTransform;
        bool isCaughtUp;
        private void OnEnable()
        {
            objectCollisionDetector.OnObjectCollisionEnter += ObjectCollisionDetector_OnObjectCollisionEnter; ;
            objectCollisionDetector.OnObjectCollisionExit += ObjectCollisionDetector_OnObjectCollisionExit;
        }

        private void ObjectCollisionDetector_OnObjectCollisionEnter(GameObject obj)
        {
            if (_targetTransform == obj.transform)
                isCaughtUp = true;

            MoveBehaviour.StopMove();
        }

        private void ObjectCollisionDetector_OnObjectCollisionExit(GameObject obj)
        {
            if (_targetTransform == null)
                return;

            if (obj.transform == _targetTransform)
            {
                isCaughtUp = false;
            }
        }

        public override bool Follow(FindTargetBehaviour findTargetBehaviour)
        {
            if (findTargetBehaviour.MostTargets.Count == 0)
            {
                return false;
            }
            if (!isCaughtUp)
            {
                MoveBehaviour.Move();
            }
            var targetTransform = findTargetBehaviour.MostTargets.First().transform;
            _targetTransform = targetTransform;
            return isCaughtUp;
        }
    }
}