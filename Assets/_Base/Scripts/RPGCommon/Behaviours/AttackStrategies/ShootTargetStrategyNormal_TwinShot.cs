using _Base.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class ShootTargetStrategyNormal_TwinShot: ShootTargetStrategy_Normal
    {
        public float gap = 100f;
        public int amount = 3;
        
        public override void DoAttack()
        {
            var centerDirection = CalculateShootDirection();

            var mostLeftX = amount / 2 * gap;
            if (amount % 2 == 0)
            {
                mostLeftX -= gap / 2;
            }
            
            Quaternion rotation1 = Quaternion.AngleAxis(10f, Vector3.up);
            
            var mostLeftDirection = centerDirection * centerDirection.Rotate(-90);

            for (var idx = 0; idx < amount; idx++)
            {
                var projectile = SpawnProjectile(centerDirection);
                projectile.transform.Translate(projectile.transform.right * (-mostLeftX + gap * idx), Space.World);
                projectile.moveSpeed.BaseValue = 100;
            }
        }
    }
}