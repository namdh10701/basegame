using _Base.Scripts.RPG.Entities;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ShootTargetStrategyNormal_BouncingShot: ShootTargetStrategy_Normal
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
            public ShootTargetStrategyNormal_BouncingShot strategy;
            public int bounceCount;

            public BouncingShotCollisionHandler(ShootTargetStrategyNormal_BouncingShot strategy)
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