using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Behaviours.CheckCollidableTarget;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Base.Scripts.RPG.Behaviours.ShootTarget
{
    public class ShootTargetBehaviour: MonoBehaviour
    {
        public ShootAccuracy shootAccuracy;
        public Transform shootPosition;
        public Projectile projectilePrefab;
        public AimTargetBehaviour aimTargetBehaviour;
        [FormerlySerializedAs("collisionTargetChecker")] [FormerlySerializedAs("collisionChecker")] [FormerlySerializedAs("collissionChecker")] [FormerlySerializedAs("collidableChecker")] public CollidedTargetChecker collidedTargetChecker;
        public void Shoot()
        {
            if (!aimTargetBehaviour.IsReadyToFire)
            {
                return;
            }
            Quaternion shootDirection = CalculateShootDirection();
            // ShootProjectile(shootDirection);
            
            var projectile = Instantiate(projectilePrefab, shootPosition.position, shootDirection, null);
            // var projectile = Instantiate(projectilePrefab);
            // projectile.transform.position = shootPosition.position;
            // projectile.transform.rotation = shootDirection;
            projectile.moveSpeed.Value = 100;
            projectile.collidedTargetChecker = collidedTargetChecker;
        }
        
        private Quaternion CalculateShootDirection()
        {
            var targetPosition = aimTargetBehaviour.LockedPosition;
            targetPosition.x += Random.Range(-shootAccuracy.Value, shootAccuracy.Value);
            targetPosition.y += Random.Range(-shootAccuracy.Value, shootAccuracy.Value);

            var direction = targetPosition - shootPosition.position;
            var aimDirection = Quaternion.LookRotation(Vector3.forward, direction);
            return aimDirection;
        }
    }
}