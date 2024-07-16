using _Base.Scripts.RPGCommon.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ElectricEelProjectile : Projectile
    {
        public Transform startTransform;
        public Transform targetTransform;
        public ElectricFx lightingFx;
        public Transform TargetTransform { set { targetTransform = value; lightingFx.targetTransform = value; } }
        public Transform StartTransform { set { startTransform = value; lightingFx.startTransform = value; } }
        private void Start()
        {
            ApplyStats();
        }
        public override void ApplyStats()
        {
            base.ApplyStats();
            ProjectileMovement = new HomingMove(this, targetTransform);
            ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)CollisionHandler;
            projectileCollisionHandler.Handlers.Add(new ElectricBounceHandler(10, 20, lightingFx));
        }

        private void FixedUpdate()
        {
            ProjectileMovement.Move();
        }

        private void OnDestroy()
        {
            Debug.Log("DESTROY 1");
        }
    }

    public class ElectricBounceHandler : IHandler
    {
        int bounceCount = 0;
        int maxBounce;
        float range;
        ElectricFx electricFxPrefab;
        public LayerMask CrewLayer;
        public ElectricBounceHandler(int maxBounce, float range, ElectricFx electricFxPrefab)
        {
            this.maxBounce = maxBounce;
            this.range = range;
            this.electricFxPrefab = electricFxPrefab;
        }

        public bool IsCompleted => bounceCount == maxBounce;

        public void Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
        {
            List<IEffectTaker> inRangeEntities = new List<IEffectTaker>();
            RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(collidedEntity.Transform.position, range, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Crew"));

            foreach (RaycastHit2D hit in inRangeColliders)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.TryGetComponent(out EffectTakerCollider effectTakerCollider))
                {
                    if (!((ProjectileCollisionHandler)p.CollisionHandler).IgnoreCollideEntities.Contains(effectTakerCollider.Taker))
                    {
                        inRangeEntities.Add(effectTakerCollider.Taker);
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
                ((HomingMove)p.ProjectileMovement).target = nextTarget.Transform;
                ElectricFx nextProjectile = GameObject.Instantiate(electricFxPrefab);
                nextProjectile.transform.position = collidedEntity.Transform.position;
                nextProjectile.startTransform = collidedEntity.Transform;
                nextProjectile.targetTransform = nextTarget.Transform;
                nextProjectile.Play();
            }
            else
            {
                bounceCount = maxBounce;
            }

        }


    }
}