using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.Utils.Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public abstract class Projectile: Entity
    {
        public Rigidbody2D body;
        
        public Stat moveSpeed;

        public FindTargetStrategy findTargetStrategy;
        
        private void Start()
        {
            body.velocity = transform.up * moveSpeed.Value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!findTargetStrategy.TryGetTargetEntity(collision.gameObject, out var found))
            {
                return;
            }
            OnCollisionEnter(found);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        protected void OnCollisionEnter(Entity entity)
        {
            foreach (var effect in OutgoingEffects)
            {
                if (entity.Stats is not IAlive)
                {
                    continue;
                }

                entity.AddCarryingEffect<>()
                effect.Process();
                // entity.effectHolder.gameObject.AddComponent((Effect)effect);//.Process();
            }
            Destroy(gameObject);
        }
    }
}