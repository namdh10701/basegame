using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.CheckCollidableTarget;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public abstract class Projectile: MonoBehaviour
    {
        public Rigidbody2D body;
        
        public MoveSpeed moveSpeed;

        public CollidedTargetChecker collidedTargetChecker;
        
        private void Awake()
        {
            body.velocity = transform.up * moveSpeed.Value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collidedTargetChecker.Check(collision))
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