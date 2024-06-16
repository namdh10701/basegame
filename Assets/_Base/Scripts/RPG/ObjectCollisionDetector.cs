using System;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    [RequireComponent(typeof(Collider2D))]
    public class ObjectCollisionDetector : MonoBehaviour
    {
        CircleCollider2D CircleCollider;
        public event Action<GameObject> OnObjectCollisionEnter;
        public event Action<GameObject> OnObjectCollisionExit;
        private void Awake()
        {
            CircleCollider = GetComponent<CircleCollider2D>();
            CircleCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnObjectCollisionEnter?.Invoke(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnObjectCollisionExit?.Invoke(collision.gameObject);
        }

        public void SetRadius(float radius)
        {
            if (CircleCollider == null)
            {
                CircleCollider = GetComponent<CircleCollider2D>();
            }
            CircleCollider.radius = radius;
        }
    }
}