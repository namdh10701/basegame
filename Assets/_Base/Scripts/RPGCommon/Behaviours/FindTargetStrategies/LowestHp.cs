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
        public override bool TryGetTargetEntity(GameObject go, out EffectTakerCollider entity)
        {
            go.TryGetComponent<IAliveStats>(out var found);
            entity = found as EffectTakerCollider;
            return entity != null;
        }

        [CanBeNull]
        public override List<EffectTakerCollider> FindTheMostTargets(List<EffectTakerCollider> foundTargets)
        {
            EffectTakerCollider lowestHpTarget = null;
            var lowestHp = Mathf.Infinity;

            foreach (var entity in foundTargets)
            {
                var livingEntity = (IAliveStats)entity;
                // float hp = livingEntity.HealthPoint.CalculatedValue;
                // if (hp < lowestHp)
                // {
                //     lowestHp = hp;
                //     lowestHpTarget = entity;
                // }
            }

            return new List<EffectTakerCollider> { lowestHpTarget };
        }
    }
}