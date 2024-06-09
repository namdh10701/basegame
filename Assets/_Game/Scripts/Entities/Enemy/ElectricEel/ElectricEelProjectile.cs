using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEelProjectile : Projectile
{
    public Transform startTransform;
    public Transform targetTransform;
    public ElectricFx lightingFx;
    public Transform TargetTransform { set { targetTransform = value; lightingFx.targetTransform = value; } }
    public Transform StartTransform { set { startTransform = value; lightingFx.startTransform = value; } }

    protected override void Awake()
    {
        base.Awake();
        ProjectileMovement = new HomingMove(this, targetTransform);
        ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)CollisionHandler;
        projectileCollisionHandler.Handlers.Add(new ElectricBounceHandler(3, 10, lightingFx));
    }

    private void FixedUpdate()
    {
        ProjectileMovement.Move();
    }
}

public class ElectricBounceHandler : IHandler
{
    int bounceCount = 0;
    int maxBounce;
    float range;
    ElectricFx electricFxPrefab;

    public ElectricBounceHandler(int maxBounce, float range, ElectricFx electricFxPrefab)
    {
        this.maxBounce = maxBounce;
        this.range = range;
        this.electricFxPrefab = electricFxPrefab;
    }

    public bool IsCompleted => bounceCount == maxBounce;

    public void Process(Projectile p, Entity mainEntity, Entity collidedEntity)
    {
        List<Entity> inRangeEntities = new List<Entity>();
        RaycastHit2D[] inRangeColliders = Physics2D.CircleCastAll(collidedEntity.transform.position, range, Vector2.zero);
        foreach (RaycastHit2D hit in inRangeColliders)
        {
            if (hit.collider.TryGetComponent(out EntityCollisionDetector entityCollisionDetector))
            {
                Entity entity = entityCollisionDetector.GetComponent<EntityProvider>().Entity;


                if (!((ProjectileCollisionHandler)p.CollisionHandler).IgnoreCollideEntities.Contains(entity))
                {
                    if (entity is Entity && entity != p)
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
                float distance = Vector2.Distance(mainEntity.transform.position, entity.transform.position);
                if (distance < minDistance)
                {
                    nextTarget = entity;
                    minDistance = distance;
                }
            }

            bounceCount++;
            ((HomingMove)p.ProjectileMovement).target = nextTarget.transform;
            ElectricFx nextProjectile = GameObject.Instantiate(electricFxPrefab);
            nextProjectile.transform.position = collidedEntity.transform.position;
            nextProjectile.startTransform = collidedEntity.transform;
            nextProjectile.targetTransform = nextTarget.transform;
            nextProjectile.Play();
        }
        else
        {
            bounceCount = maxBounce;
        }

    }
}