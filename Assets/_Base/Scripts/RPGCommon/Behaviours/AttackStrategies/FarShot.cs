using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static _Base.Scripts.RPGCommon.Behaviours.AttackStrategies.BouncingShot;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    public class FarShot : NormalShot
    {
        public override void DoAttack()
        {
            Debug.Log("Far shot attack");
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);
            IncreaseDmgOverTime modifier = projectile.gameObject.AddComponent<IncreaseDmgOverTime>();
            Stat dmg = projectile._stats.Damage;
            modifier.Init(dmg, 2);
            projectile.ProjectileMovement = new StraightMove(projectile);
        }
    }

}
