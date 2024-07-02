using System;
using UnityEngine;

namespace _Base.Scripts.RPG
{
    [RequireComponent(typeof(Collider2D))]
    public class EffectCollisionDetector : MonoBehaviour
    {

        public event Action<IEffectTaker> OnEntityCollisionEnter;
        public event Action<IEffectTaker> OnEntityCollisionExit;

        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var entityBody = collision.GetComponent<IEffectTakerCollider>();
            if (entityBody == null)
            {
                return;
            }
            OnEntityCollisionEnter?.Invoke(entityBody.Taker);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var entityBody = collision.GetComponent<IEffectTakerCollider>();

            if (entityBody == null)
            {
                return;
            }
            OnEntityCollisionExit?.Invoke(entityBody.Taker);
        }
    }
}