using System;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Behaviours.FindTargetStrategies;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] BouncingShot")]
    public class BouncingShot : NormalShot
    {

        public float lookupRange = 100f;
        public int bounceTimes = 0;
        public float damageScale = 0;
        Entity oldCollidedEntity;
        public System.Action<Entity, int> OnCollisionEnter;

        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);

            projectile.CollisionHandler = new BouncingShotCollisionHandler(this);
            OnCollisionEnter += (mainEntity, count) => SpawnCouncingShot(mainEntity, count);
        }

        private void SpawnCouncingShot(Entity mainEntity, int count)
        {
            // find next target
            var projectile = mainEntity.gameObject.GetComponentInChildren<Projectile>();
            var findTargetBehaviour = mainEntity.gameObject.GetComponentInChildren<FindTargetBehaviour>();
            if (findTargetBehaviour.Targets.Count <= count)
            {
                Destroy(mainEntity.gameObject);
            }
            else
            {
                var direction = findTargetBehaviour.Targets[count].transform.position - findTargetBehaviour.Targets[count - 1].transform.position;
                projectile.body.velocity = direction.normalized * projectile.moveSpeed.Value;
            }

        }

        class BouncingShotCollisionHandler : DefaultCollisionHandler
        {
            public BouncingShot strategy;
            public int bounceCount;

            public BouncingShotCollisionHandler(BouncingShot strategy)
            {
                this.strategy = strategy;
            }

            public override void Process(Entity mainEntity, Entity collidedEntity)
            {
                strategy.oldCollidedEntity = collidedEntity;
                base.Process(mainEntity, collidedEntity);
                if (++bounceCount <= 3)
                {
                    strategy.OnCollisionEnter?.Invoke(mainEntity, bounceCount);
                    // Destroy(mainEntity.gameObject);
                    return;
                }
            }
        }
    }
}