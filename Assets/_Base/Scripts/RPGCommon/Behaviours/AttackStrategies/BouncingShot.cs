using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] BouncingShot")]
    public class BouncingShot: NormalShot
    {
        
        public float lookupRange = 100f;
        public int bounceTimes = 3;
        public float damageScale = 0;
        
        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection);

            projectile.CollisionHandler = new BouncingShotCollisionHandler(this);
        }
        
        
        class BouncingShotCollisionHandler: DefaultCollisionHandler
        {
            public BouncingShot strategy;
            public int bounceCount;

            public BouncingShotCollisionHandler(BouncingShot strategy)
            {
                this.strategy = strategy;
            }

            public override void Process(Entity mainEntity, Entity collidedEntity)
            {
                base.Process(mainEntity, collidedEntity);

                if (--bounceCount <= 0)
                {
                    Destroy(mainEntity.gameObject);
                    return;
                }
                
                // find next target
                
                // follow target
            }
        }
    }
}