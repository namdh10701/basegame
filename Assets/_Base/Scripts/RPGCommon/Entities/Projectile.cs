using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public abstract class Projectile: Entity
    {
        public Rigidbody2D body;
        
        public MoveSpeed moveSpeed;

        public FindTargetStrategy findTargetStrategy;
        
        private void Awake()
        {
            body.velocity = transform.up * moveSpeed.Value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!findTargetStrategy.TryGetTargetEntity(collision.gameObject, out var found))
            {
                return;
            }
            OnHit();
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        protected void OnHit()
        {
            Destroy(gameObject);
        }
    }
}