using _Base.Scripts.RPG.Stats;
using _Game.Features.Gameplay;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class FarShot : NormalShot
    {
        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);
            IncreaseDmgOverTime modifier = projectile.gameObject.AddComponent<IncreaseDmgOverTime>();
            Stat dmg = projectile._stats.Damage;
            modifier.Init(dmg, 2);
            projectile.ProjectileMovement = new StraightMove(projectile);
        }
    }

}
