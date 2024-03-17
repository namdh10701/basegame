using JetBrains.Annotations;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FindTarget.Strategy
{
    [AddComponentMenu("RPG/FindTargetStrategy/[FindTargetStrategy] LowestHp")]
    public class LowestHp: FindTargetStrategy
    {
        public override Entity GetTarget(GameObject collisionGameObject)
        {
            var target = collisionGameObject.GetComponent<Entity>();

            return target is not ILivingEntity ? null : target;
        }

        [CanBeNull]
        public override Entity FindTheMostTarget(FindTargetBehaviour findTargetBehaviour)
        {
            Entity lowestHpTarget = null;
            var lowestHp = Mathf.Infinity;

            foreach (var entity in findTargetBehaviour.Targets)
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