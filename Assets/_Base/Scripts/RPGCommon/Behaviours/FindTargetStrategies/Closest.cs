using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Unity.EditorUsedAttributes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Base.Scripts.RPGCommon.Behaviours.FindTargetStrategies
{
    [AddComponentMenu("RPG/FindTargetStrategy/[FindTargetStrategy] Closest")]
    public class Closest : FindTargetStrategy
    {
        [MonoScript(typeof(Entity))]
        public List<string> targetTypeNames;

        public Transform closestTo;

        private void Awake()
        {
            Assert.IsNotNull(closestTo);
        }

        public override bool TryGetTargetEntity(GameObject go, out Entity entity)
        {
            go.TryGetComponent<Entity>(out var found);
            entity = found;
            return found != null && targetTypeNames.Contains(found.GetType().FullName);
        }

        [CanBeNull]
        public override List<Entity> FindTheMostTargets(List<Entity> foundTargets)
        {
            Entity closestTarget = null;
            var closestDistance = Mathf.Infinity;
            Vector2 currentPosition = closestTo.transform.position;

            foreach (Entity entity in foundTargets)
            {
                Vector2 targetPosition = entity.transform.position;
                float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = entity;
                }
            }

            var results = new List<Entity>();

            if (closestTarget != null)
            {
                results.Add(closestTarget);
            }

            return results;
        }
    }
}