using _Base.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] TwinShot")]
    public class TwinShot : NormalShot
    {
        public float gap = 100f;

        public override void DoAttack()
        {
            var centerDirection = CalculateShootDirection();

            var mostLeftX = NumOfProjectile / 2 * gap;
            if (NumOfProjectile % 2 == 0)
            {
                mostLeftX -= gap / 2;
            }

            Quaternion rotation1 = Quaternion.AngleAxis(10f, Vector3.up);

            var mostLeftDirection = centerDirection * centerDirection.Rotate(-90);

            for (var idx = 0; idx < NumOfProjectile; idx++)
            {
                var projectile = SpawnProjectile(centerDirection, shootPosition);
                projectile.transform.Translate(projectile.transform.right * (-mostLeftX + gap * idx), Space.World);
                //projectile.moveSpeed.BaseValue = 100;
            }
        }
    }
}