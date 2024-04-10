using UnityEngine;

namespace Demo.Scripts.Ship
{
    public class ShipCollider : MonoBehaviour
    {
        public float Hp;
        public float BlockChance;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnemyProjectile"))
            {
                Destroy(collision.gameObject);
                Hp -= 15;
            }
        }

    }
}
