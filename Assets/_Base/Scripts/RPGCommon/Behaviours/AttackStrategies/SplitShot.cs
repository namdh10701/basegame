using _Base.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] SplitShot")]
    public class SplitShot : NormalShot
    {
        public float angle = 15f;
        public int amount = 3;

        public override void DoAttack()
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
                var projectile = SpawnProjectile(shootDirection, shootPosition);
            }
        }
    }
}