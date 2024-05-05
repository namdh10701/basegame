using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] Duplicate Shot")]
    public class DuplicateShot : NormalShot
    {
        public float angle = 15f;
        public int amount = 3;
        Entity oldMainEntity;
        Entity oldcollidedEntity;

        public System.Action<Entity, Entity> OnCollisionEnter;

        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);

            projectile.CollisionHandler = new DuplicateShotCollisionHandler(this);
            OnCollisionEnter += (mainEntity, collidedEntity) => SpawnDuplicateShot(mainEntity, collidedEntity);

        }

        public void SpawnDuplicateShot(Entity mainEntity, Entity collidedEntity)
        {
            oldMainEntity = mainEntity;
            oldcollidedEntity = collidedEntity;
            var centerDirection = CalculateShootDirection();

            var mostLeftAngle = amount / 2 * angle;
            if (amount % 2 == 0)
            {
                mostLeftAngle -= angle / 2;
            }
            var mostLeftDirection = centerDirection.Rotate(-mostLeftAngle);

            for (var idx = 0; idx < amount; idx++)
            {
                var shootDirection = mostLeftDirection.Rotate(idx * angle);
                var projectile = SpawnProjectile(shootDirection, collidedEntity.transform);
                projectile.transform.localPosition = collidedEntity.transform.position;
                projectile.CollisionHandler = new AfterDuplicateShotCollisionHandler(this);
            }
        }

        class DuplicateShotCollisionHandler : DefaultCollisionHandler
        {
            public DuplicateShot Strategy;

            public DuplicateShotCollisionHandler(DuplicateShot strategy)
            {
                Strategy = strategy;
            }

            public override void Process(Entity mainEntity, Entity collidedEntity)
            {
                base.Process(mainEntity, collidedEntity);
                Strategy.OnCollisionEnter?.Invoke(mainEntity, collidedEntity);
                Object.Destroy(mainEntity.gameObject);

            }

        }

        class AfterDuplicateShotCollisionHandler : DefaultCollisionHandler
        {
            public DuplicateShot Strategy;

            public AfterDuplicateShotCollisionHandler(DuplicateShot strategy)
            {
                Strategy = strategy;
            }

            public override void Process(Entity mainEntity, Entity collidedEntity)
            {
                if (Strategy.oldcollidedEntity == collidedEntity) return;
                base.Process(mainEntity, collidedEntity);
                Object.Destroy(mainEntity.gameObject);


            }

        }
    }
}