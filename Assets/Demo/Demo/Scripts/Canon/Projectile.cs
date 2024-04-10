using Demo.ScriptableObjects.Scripts;
using UnityEngine;

namespace Demo.Scripts.Canon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Rigidbody2D body;
        [SerializeField] public AmmoData AmmoData;
        private void Start()
        {
            body.velocity = transform.up * AmmoData.Speed / 25;
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