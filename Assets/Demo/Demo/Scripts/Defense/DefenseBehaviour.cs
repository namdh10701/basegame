using Demo.ScriptableObjects.Scripts;
using Demo.Scripts.Canon;
using UnityEngine;

namespace Demo.Scripts.Defense
{
    public class DefenseBehaviour : MonoBehaviour
    {
        public DefenseData DefenseData;

        string bulletLayerName = "PlayerProjectile";
        private int bulletLayer;

        private void Start()
        {
            bulletLayer = LayerMask.NameToLayer(bulletLayerName);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == bulletLayer)
            {
                Projectile projectile = collision.GetComponent<Projectile>();
                projectile.OnHit();
            }
        }


    }
}