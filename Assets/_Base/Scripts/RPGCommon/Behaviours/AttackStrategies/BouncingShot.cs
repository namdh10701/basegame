using System;
using System.Collections.Generic;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Behaviours.FindTargetStrategies;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using Unity.VisualScripting;
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
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);
            ((ProjectileCollisionHandler)projectile.CollisionHandler).Handlers.Add(new BouncingHandler(bounceTimes, lookupRange));
            projectile.ProjectileMovement = new HomingMove(projectile, targetBehaviour.MostTargets[0].transform);
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
                RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(collidedEntity.Transform.position, range, Vector2.zero);
                foreach (RaycastHit2D hit in inRangeColliders)
                {
                    if (hit.collider.TryGetComponent(out EffectCollisionDetector entityCollisionDetector))
                    {
                        Entity entity = entityCollisionDetector.GetComponent<EntityProvider>().Entity;


                        if (!((ProjectileCollisionHandler)p.CollisionHandler).IgnoreCollideEntities.Contains(collidedEntity))
                        {
                            if (entity is Enemy)
                            {
                                inRangeEntities.Add(entity);
                            }
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
                    ((HomingMove)p.ProjectileMovement).target = nextTarget.transform;
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