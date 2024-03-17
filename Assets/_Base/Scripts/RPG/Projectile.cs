using System;
using _Base.Scripts.RPG.Attributes;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    public abstract class Projectile: MonoBehaviour
    {
        [SerializeField] Rigidbody2D body;
        public MoveSpeed moveSpeed;
        
        private void Start()
        {
            body.velocity = transform.up * moveSpeed.Value / 25;
        }

        private void Update()
        {
            // body.AddForce();
            body.velocity = transform.up * moveSpeed.Value / 25;
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        public void OnHit()
        {
            Destroy(gameObject);
        }
    }
}