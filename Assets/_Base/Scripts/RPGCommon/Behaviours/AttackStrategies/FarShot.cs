using _Base.Scripts.RPG.Effects;
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
            foreach (Effect effect in projectile.OutGoingEffects)
            {
                if (effect is DecreaseHealthEffect dhe)
                {
                    modifier.Init(dhe, 5);
                    break;
                }
            }
            

            projectile.ProjectileMovement = new StraightMove(projectile);
        }
    }

}
