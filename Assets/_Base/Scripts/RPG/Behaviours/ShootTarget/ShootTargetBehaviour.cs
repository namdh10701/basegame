using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.AimTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.ShootTarget
{
    public class ShootTargetBehaviour: MonoBehaviour
    {
        public ShootAccuracy shootAccuracy;
        public Transform shootPosition;
        public Projectile projectilePrefab;
        public AimTargetBehaviour aimTargetBehaviour;
        
        public void Shoot()
        {
            Quaternion shootDirection = CalculateShootDirection();
            // ShootProjectile(shootDirection);
            
            var projectile = Instantiate(projectilePrefab, shootPosition.position, shootDirection, null);
        }
        
        private Quaternion CalculateShootDirection()
        {
            var aimDirection = aimTargetBehaviour.LockedPosition - shootPosition.position;
            // var aimDirection = Quaternion.LookRotation(Vector3.forward, targetDirection);
            aimDirection.z += Random.Range(-shootAccuracy.Value, shootAccuracy.Value);
            Quaternion shootDirection = Quaternion.Euler(aimDirection);
            return shootDirection;
        }
    }
}