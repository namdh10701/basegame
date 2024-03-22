using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.FindTargetStrategies
{
    [AddComponentMenu("RPG/FindTargetStrategy/[FindTargetStrategy] LowestHp")]
    public class LowestHp: FindTargetStrategy
    {
        public override bool TryGetTargetEntity(GameObject go, out Entity entity)
        {
            go.TryGetComponent<ILivingEntity>(out var found);
            entity = found as Entity;
            return entity != null;
        }

        [CanBeNull]
        public override Entity FindTheMostTarget(List<Entity> foundTargets)
        {
            Entity lowestHpTarget = null;
            var lowestHp = Mathf.Infinity;

            foreach (var entity in foundTargets)
            {
                var livingEntity = (ILivingEntity)entity;
                float hp = livingEntity.HealthPoint.Value;
                if (hp < lowestHp)
                {
                    lowestHp = hp;
                    lowestHpTarget = entity;
                }
            }

            return lowestHpTarget;
        }
    }
}