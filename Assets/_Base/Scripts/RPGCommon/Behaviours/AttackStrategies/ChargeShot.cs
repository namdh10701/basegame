using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] ChargeShot")]
    public class ChargeShot : NormalShot
    {
        public float minSpeedBulletModifier = .8f;
        public float maxSpeedBulletModifier = 1f;

        public override void DoAttack()
        {
            for (var idx = 0; idx < NumOfProjectile; idx++)
            {
                CannonProjectile cannonProjectile = projectilePrefab as CannonProjectile;
                float projectileAcc = cannonProjectile._stats.Accuracy.Value;
                float totalAccuaracy = ((CannonStats)Cannon.Stats).AttackAccuracy.Value + projectileAcc;

                float minAngle = -totalAccuaracy;
                float maxAngle = +totalAccuaracy;

                float randomAngle = Random.Range(minAngle, maxAngle);
                var shootDirection = transform.rotation.Rotate(randomAngle);
                var projectile = SpawnProjectile(shootDirection, shootPosition);
                AddProjectileModifiers(projectile);
                float speedMod = Random.Range(.8f, 1f);
                projectile._stats.Speed.BaseValue *= speedMod;
            }
        }
    }
}