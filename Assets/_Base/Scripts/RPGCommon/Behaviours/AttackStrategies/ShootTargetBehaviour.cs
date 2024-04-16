using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ShootTargetBehaviour: AttackTargetBehaviour
    {
        public Stat attackAccuracy;
        public Transform shootPosition;
        public Entities.Projectile projectilePrefab;
        // public CollidedTargetChecker collidedTargetChecker;

        protected override void DoAttack()
        {
            Quaternion shootDirection = CalculateShootDirection();
            
            var projectile = Instantiate(projectilePrefab, shootPosition.position, shootDirection, null);
            projectile.moveSpeed.BaseValue = 100;
            projectile.findTargetStrategy = aimTargetBehaviour.FollowTargetBehaviour.FindTargetBehaviour.Strategy;
            
            // var dec = new GameObject().AddComponent<DecreaseHealthPointEffect>();
            // dec.Amount = 100;
            // projectile.AddCarryingEffect<DecreaseHealthPointEffect>().Amount = 100;
            // projectile.OutgoingEffects.Add(new DecreaseHealthEffect(100));
            projectile.OutgoingEffects.Add(new DrainHealthEffect(50, 1, 3));
        }
        
        protected virtual Quaternion CalculateShootDirection()
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