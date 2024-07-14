using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] BouncingShot")]
    public class BouncingShot : NormalShot
    {

        public float lookupRange = 100f;
        public int bounceTimes = 0;
        public FindTargetBehaviour targetBehaviour;
        public override void DoAttack()
        {
            CannonStats cannonStats = Cannon.Stats as CannonStats;
            bounceTimes = (int)cannonStats.ProjectileCount.BaseValue;
            float primaryDmg = cannonStats.PrimaryDamage.Value;
            float secondaryDmg = cannonStats.SecondaryDamage.Value;
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);

            ProjectileStats projectileStats = projectile.Stats as ProjectileStats;
            projectile.SetDamage(projectileStats.Damage.Value * primaryDmg, projectile.isCrit);

            float secondaryDmgCalculated = projectileStats.Damage.Value * secondaryDmg;
            ((ProjectileCollisionHandler)projectile.CollisionHandler).Handlers.Add(new BouncingHandler(bounceTimes, secondaryDmgCalculated, lookupRange));
            projectile.ProjectileMovement = new StraightMove(projectile);
        }
        public override void Consume(RangedStat ammo)
        {
            int numOfProjectileCanProvide = (int)ammo.Value;
            ActualNumOfProjectile = Mathf.Min(numOfProjectileCanProvide, (int)Mathf.Max(1, 1));
            ammo.StatValue.BaseValue -= ActualNumOfProjectile;
        }
        public class BouncingHandler : IHandler
        {
            public int bounceCount;
            private float range;
            private int maxBounce;
            public List<Entity> collided;
            float dmg;
            public bool IsCompleted => bounceCount == maxBounce;

            public BouncingHandler(int maxBounce, float dmg, float range)
            {
                this.dmg = dmg;
                this.maxBounce = maxBounce;
                this.range = range;
            }

            void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
            {
                List<IEffectTaker> inRangeEntities = new List<IEffectTaker>();
                RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(collidedEntity.Transform.position, range, Vector2.zero, LayerMask.NameToLayer("Enemy"));
                foreach (RaycastHit2D hit in inRangeColliders)
                {
                    if (hit.collider.TryGetComponent(out EffectTakerCollider entity))
                    {

                        if (!((ProjectileCollisionHandler)p.CollisionHandler).IgnoreCollideEntities.Contains(entity.Taker))
                        {
                            if (entity.Taker is EnemyModel)
                            {
                                inRangeEntities.Add(entity.Taker);
                            }
                        }
                    }
                }

                if (inRangeEntities.Count > 0)
                {
                    IEffectTaker nextTarget = inRangeEntities[0];
                    float minDistance = Mathf.Infinity;
                    foreach (IEffectTaker entity in inRangeEntities)
                    {
                        float distance = Vector2.Distance(mainEntity.Transform.position, entity.Transform.position);
                        if (distance < minDistance)
                        {
                            nextTarget = entity;
                            minDistance = distance;
                        }
                    }

                    bounceCount++;
                    p.SetDamage(dmg, p.isCrit);
                    p.ProjectileMovement = new HomingMove(p, nextTarget.Transform);
                }
                else
                {
                    bounceCount = maxBounce;
                }
            }
        }
    }
}