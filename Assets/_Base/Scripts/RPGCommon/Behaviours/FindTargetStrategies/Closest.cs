using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace _Base.Scripts.RPGCommon.Behaviours.FindTargetStrategies
{
    [AddComponentMenu("RPG/FindTargetStrategy/[FindTargetStrategy] Closest")]
    public class Closest: FindTargetStrategy
    {
        public Transform closestTo;

        private void Awake()
        {
            Assert.IsNotNull(closestTo);
        }

        public override bool TryGetTargetEntity(GameObject go, out Entity entity)
        {
            go.TryGetComponent<Entity>(out var found);
            entity = found;
            return found != null;
        }

        [CanBeNull]
        public override Entity FindTheMostTarget(List<Entity> foundTargets)
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

            return closestTarget;
        }
    }
}