using System.Collections.Generic;
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] ChargeShot")]
    public class ChargeShot : NormalShot
    {
        public float minAngle = -20f;
        public float maxAngle = 20f;
        public float minSpeedBulletModifier = -1;
        public float maxSpeedBulletModifier = -10;
        public int amount = 3;

        public override void DoAttack()
        {
            for (var idx = 0; idx < amount; idx++)
            {
                float randomAngle = Random.Range(minAngle, maxAngle);
                var shootDirection = transform.rotation.Rotate(randomAngle);
                var projectile = SpawnProjectile(shootDirection, shootPosition);
                AddProjectileModifiers(projectile);
                projectile.AddMoveSpeed(new RPG.Stats.StatModifier(Random.Range(minSpeedBulletModifier, maxSpeedBulletModifier), RPG.Stats.StatModType.Flat, 1));
            }
        }
    }
}