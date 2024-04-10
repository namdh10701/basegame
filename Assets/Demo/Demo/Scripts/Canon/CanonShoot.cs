using UnityEngine;

namespace Demo.Scripts.Canon
{
    public class CanonShoot : MonoBehaviour
    {
        Canon canon;
        public Projectile projectilePrefab;
        public Transform shootPos;
        public Transform visual;
        public float Accuracy { get; set; }

        private void Awake()
        {
            canon = GetComponent<Canon>();
        }
        public void Shoot(Transform transform)
        {
            Quaternion shootDirection = CalculateShootDirection();
            ShootProjectile(shootDirection);
        }

        Quaternion CalculateShootDirection()
        {
            Vector3 aim = visual.rotation.eulerAngles;
            aim.z += Random.Range(-Accuracy, Accuracy);
            Quaternion shootDirection = Quaternion.Euler(aim);
            return shootDirection;
        }

        void ShootProjectile(Quaternion shootDirection)
        {
            Projectile projectile = Instantiate(projectilePrefab, shootPos.position, shootDirection, null);

            DamageSource damageSource = projectile.GetComponent<DamageSource>();
            damageSource.Attack += canon.CanonData.Attack;
            damageSource.Attack += projectile.AmmoData.Attack;
            damageSource.AOE = projectile.AmmoData.AOE;
            damageSource.CritChance += canon.CanonData.CritChance;
            damageSource.CritChance += projectile.AmmoData.CritChance;
            damageSource.CritDamage += canon.CanonData.CritDamage;
            damageSource.CritDamage += projectile.AmmoData.CritDamage;
        }
    }
}