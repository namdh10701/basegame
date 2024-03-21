using _Base.Scripts.RPG.Attributes;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ShootTargetBehaviour: AttackTargetBehaviour
    {
        public AttackAccuracy attackAccuracy;
        public Transform shootPosition;
        public Entities.Projectile projectilePrefab;
        // public CollidedTargetChecker collidedTargetChecker;

        protected override void DoAttack()
        {
            Quaternion shootDirection = CalculateShootDirection();
            
            var projectile = Instantiate(projectilePrefab, shootPosition.position, shootDirection, null);
            projectile.moveSpeed.Value = 100;
            projectile.findTargetStrategy = aimTargetBehaviour.FollowTargetBehaviour.FindTargetBehaviour.Strategy;
        }
        
        private Quaternion CalculateShootDirection()
        {
            var targetPosition = aimTargetBehaviour.LockedPosition;
            targetPosition.x += Random.Range(-attackAccuracy.Value, attackAccuracy.Value);
            targetPosition.y += Random.Range(-attackAccuracy.Value, attackAccuracy.Value);

            var direction = targetPosition - shootPosition.position;
            var aimDirection = Quaternion.LookRotation(Vector3.forward, direction);
            return aimDirection;
        }
    }
}