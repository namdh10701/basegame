using Demo.Scripts.Canon;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public class Squid : Enemy
    {
        [SerializeField] Projectile bulletPrefab;
        [SerializeField] Transform shootPos;
        public override void DoAttack()
        {
            base.DoAttack();
            Projectile projectile = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
            projectile.transform.up = (target.position - shootPos.position).normalized;
        }
    }
}