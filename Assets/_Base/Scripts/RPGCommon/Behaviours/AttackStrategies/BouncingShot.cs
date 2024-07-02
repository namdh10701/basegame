using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
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
        public float damageScale = 0;
        public FindTargetBehaviour targetBehaviour;
        public override void DoAttack()
        {
            bounceTimes = (int)((CannonStats)Cannon.Stats).ProjectileCount.BaseValue;
            Debug.Log(bounceTimes);
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);
            ((ProjectileCollisionHandler)projectile.CollisionHandler).Handlers.Add(new BouncingHandler(bounceTimes, lookupRange));
            projectile.ProjectileMovement = new StraightMove(projectile);
        }

        public class BouncingHandler : IHandler
        {
            public int bounceCount;
            private float range;
            private int maxBounce;
            public List<Entity> collided;

            public bool IsCompleted => bounceCount == maxBounce;

            public BouncingHandler(int maxBounce, float range)
            {
                this.maxBounce = maxBounce;
                this.range = range;
            }

            void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
            {
                List<Entity> inRangeEntities = new List<Entity>();
                RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(collidedEntity.Transform.position, range, Vector2.zero, LayerMask.NameToLayer("Enemy"));
                foreach (RaycastHit2D hit in inRangeColliders)
                {
                    if (hit.collider.TryGetComponent(out EffectTakerCollider entityCollisionDetector))
                    {
                        Debug.Log(hit.collider);
                        Entity entity = entityCollisionDetector.GetComponent<EntityProvider>().Entity;
                        IEffectTaker taker = entity.GetComponent<IEffectTaker>();

                        if (!((ProjectileCollisionHandler)p.CollisionHandler).IgnoreCollideEntities.Contains(taker))
                        {
                            if (entity is Enemy)
                            {
                                Debug.Log("add");
                                inRangeEntities.Add(entity);
                            }
                            else
                            {
                                Debug.Log("K PHAI DAU");
                            }
                        }
                        else
                        {
                            Debug.Log("ignore ngay");
                        }
                    }
                }

                if (inRangeEntities.Count > 0)
                {
                    Entity nextTarget = inRangeEntities[0];
                    float minDistance = Mathf.Infinity;
                    foreach (Entity entity in inRangeEntities)
                    {
                        float distance = Vector2.Distance(mainEntity.Transform.position, entity.transform.position);
                        if (distance < minDistance)
                        {
                            nextTarget = entity;
                            minDistance = distance;
                        }
                    }

                    bounceCount++;
                    p.ProjectileMovement = new HomingMove(p, nextTarget.transform);
                    Debug.Log(nextTarget.name);
                }
                else
                {
                    bounceCount = maxBounce;
                }
            }
        }
    }
}