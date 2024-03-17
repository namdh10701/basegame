using JetBrains.Annotations;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FindTarget.Strategy
{
    [AddComponentMenu("RPG/FindTargetStrategy/[FindTargetStrategy] ClosetFlex")]
    public class ClosetFlex: FindTargetStrategy
    {
        public override Entity GetTarget(GameObject collisionGameObject)
        {
            return collisionGameObject.GetComponent<Entity>();
        }

        [CanBeNull]
        public override Entity FindTheMostTarget(FindTargetBehaviour findTargetBehaviour)
        {
            Entity closestTarget = null;
            var closestDistance = Mathf.Infinity;
            Vector2 currentPosition = findTargetBehaviour.transform.position;

            foreach (Entity entity in findTargetBehaviour.Targets)
            {
                Vector2 targetPosition = entity.transform.position;
                float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = entity;
                }
            }

            return closestTarget;
        }
    }
}