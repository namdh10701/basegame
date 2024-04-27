using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] SplitShot")]
    public class DuplicateShot : NormalShot
    {
        public float angle = 15f;
        public int amount = 3;

        public System.Action OnCollisionEnter;

        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection);

            projectile.CollisionHandler = new DuplicateShotCollisionHandler(this);


            // var centerDirection = CalculateShootDirection();

            // var mostLeftAngle = amount / 2 * angle;
            // if (amount % 2 == 0)
            // {
            //     mostLeftAngle -= angle / 2;
            // }
            // var mostLeftDirection = centerDirection.Rotate(-mostLeftAngle);

            // for (var idx = 0; idx < amount; idx++)
            // {
            //     var shootDirection = mostLeftDirection.Rotate(idx * angle);
            //     var projectile = SpawnProjectile(shootDirection);
            //     // var projectile = Instantiate(projectilePrefab, shootPosition.position, shootDirection, null);
            //     projectile.moveSpeed.BaseValue = 100;
            // }
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
                Strategy.OnCollisionEnter?.Invoke();

                Debug.Log("[DuplicateShotCollisionHandler]" + mainEntity.gameObject.name);
                Debug.Log("[DuplicateShotCollisionHandler]" + collidedEntity.gameObject.name);

            }

        }
    }
}