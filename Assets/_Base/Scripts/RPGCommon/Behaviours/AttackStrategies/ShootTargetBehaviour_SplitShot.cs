using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ShootTargetBehaviour_SplitShot: ShootTargetBehaviour
    {
        public float angle = 15f;
        public int amount = 3;
        
        protected override void DoAttack()
        {
            var centerDirection = CalculateShootDirection();

            var mostLeftAngle = amount / 2 * angle;
            if (amount % 2 == 0)
            {
                mostLeftAngle -= angle / 2;
            }
            var mostLeftDirection = centerDirection.Rotate(-mostLeftAngle);

            for (var idx = 0; idx < amount; idx++)
            {
                var shootDirection = mostLeftDirection.Rotate(idx * angle);
                var projectile = Instantiate(projectilePrefab, shootPosition.position, shootDirection, null);
                projectile.moveSpeed.BaseValue = 100;
                projectile.findTargetStrategy = aimTargetBehaviour.FollowTargetBehaviour.FindTargetBehaviour.Strategy;
                projectile.OutgoingEffects.Add(new DrainHealthEffect(50, 1, 3));
            }
        }
    }
}