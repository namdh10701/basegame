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

        public Transform closestTo;

        private void Awake()
        {
            Assert.IsNotNull(closestTo);
        }

        public override bool TryGetTargetEntity(GameObject go, out EffectTakerCollider entity)
        {
            go.TryGetComponent<EffectTakerCollider>(out var found);
            entity = found;
            return found != null;
        }

        [CanBeNull]
        public override List<EffectTakerCollider> FindTheMostTargets(List<EffectTakerCollider> foundTargets)
        {
            EffectTakerCollider closestTarget = null;
            var closestDistance = Mathf.Infinity;
            Vector2 currentPosition = closestTo.transform.position;

            foreach (EffectTakerCollider entity in foundTargets)
            {
                Vector2 targetPosition = entity.transform.position;
                float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = entity;
                }
            }

            var results = new List<EffectTakerCollider>();

            if (closestTarget != null)
            {
                results.Add(closestTarget);
            }

            return results;
        }
    }
}