using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using Unity.VisualScripting;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public class ProjectileCollisionHandler : DefaultCollisionHandler
    {
        public override void Process(Entity mainEntity, Entity collidedEntity)
        {
            base.Process(mainEntity, collidedEntity);
            Object.Destroy(mainEntity.gameObject);
        }
    }
    public abstract class Projectile : Entity
    {
        public Rigidbody2D body;

        public Stat moveSpeed;

        public FindTargetStrategy findTargetStrategy;

        protected Projectile()
        {
            CollisionHandler = new ProjectileCollisionHandler();
        }

        private void Start()
        {
            body.velocity = transform.up * moveSpeed.Value;
            Debug.Log("Start "+ findTargetStrategy);

            
        }

        // private void OnTriggerEnter2D(Collider2D collision)
        // {
        //     if (!findTargetStrategy.TryGetTargetEntity(collision.gameObject, out var found))
        //     {
        //         return;
        //     }
        //     OnCollisionStart(found);
        // }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        // protected void OnCollisionStart(Entity entity)
        // {
        //     foreach (var effect in OutgoingEffects)
        //     {
        //         if (entity.Stats is not IAlive)
        //         {
        //             continue;
        //         }
        //
        //         // entity.AddCarryingEffect(effect);
        //         // effect.Process();
        //         
        //         entity.EffectHandler.Apply(effect);
        //         // entity.effectHolder.gameObject.AddComponent((Effect)effect);//.Process();
        //     }
        //     Destroy(gameObject);
        // }

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("Awake"); 
            SetCollisionObjectChecker(entity => findTargetStrategy.TryGetTargetEntity(entity.gameObject, out var tmp));
        }
        
    }
}