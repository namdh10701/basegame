using System;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    [RequireComponent(typeof(Collider2D), typeof(EntityProvider))]
    public class EntityCollisionDetector: MonoBehaviour
    {
        public event Action<Entity> OnEntityCollisionEnter;
        public event Action<Entity> OnEntityCollisionExit;
        public Func<Entity, bool> CollisionObjectChecker;
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var entityBody = collision.GetComponent<EntityProvider>();

            if (entityBody == null)
            {
                return;
            }

            if (CollisionObjectChecker != null)
            {
                if (!CollisionObjectChecker.Invoke(entityBody.Entity))
                {
                    return;
                }
            }

            OnEntityCollisionEnter?.Invoke(entityBody.Entity);
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            var entityBody = collision.GetComponent<EntityProvider>();

            if (entityBody == null)
            {
                return;
            }
            OnEntityCollisionExit?.Invoke(entityBody.Entity);
        }
    }
}