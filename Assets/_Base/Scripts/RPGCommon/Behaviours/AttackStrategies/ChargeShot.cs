using System.Collections.Generic;
using _Base.Scripts.Utils.Extensions;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] ChargeShot")]
    public class ChargeShot : NormalShot
    {
        public float minAngle = 15f;
        public float maxAngle = 25f;
        public float minSpeedBullet = 3;
        public float maxSpeedBullet = 5;
        public int amount = 3;

        public List<float> angles = new List<float>();


        public override void DoAttack()
        {
            var centerDirection = CalculateShootDirection();

            var mostLeftAngle = amount / 2 * minAngle; // Sử dụng góc tối thiểu để tính toán hướng cực trái
            if (amount % 2 == 0)
            {
                mostLeftAngle -= minAngle / 2;
            }
            var mostLeftDirection = centerDirection.Rotate(-mostLeftAngle);

            for (var idx = 0; idx < amount; idx++)
            {
                // Tính toán góc ngẫu nhiên trong khoảng từ minAngle đến maxAngle
                float randomAngle = Random.Range(minAngle, maxAngle);
                var shootDirection = mostLeftDirection.Rotate(idx * randomAngle); // Sử dụng góc ngẫu nhiên
                var projectile = SpawnProjectile(shootDirection, shootPosition);
                projectile.moveSpeed.BaseValue = Random.Range(minSpeedBullet, maxSpeedBullet);
            }
        }
    }
}