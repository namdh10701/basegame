using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts.Entities;
using _Game.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using _Game.Features.Gameplay;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] SplitShot")]
    public class SplitShot : NormalShot
    {
        public float angle = 15f;
        public int amount = 3;

        public override void DoAttack()
        {
            CannonStats cannonStats = Cannon.Stats as CannonStats;
            float primaryDmg = cannonStats.PrimaryDamage.Value;
            float secondaryDmg = cannonStats.SecondaryDamage.Value;
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);

            ProjectileStats projectileStats = projectile.Stats as ProjectileStats;
            projectile.SetDamage(projectileStats.Damage.Value * primaryDmg, projectile.isCrit);

            float secondaryDmgCalculated = projectileStats.Damage.Value * secondaryDmg;
            ((ProjectileCollisionHandler)projectile.CollisionHandler).Handlers.Add(new SplitHandler(angle, secondaryDmgCalculated, (int)cannonStats.ProjectileCount.Value, shootDirection));
            projectile.ProjectileMovement = new StraightMove(projectile);

        }

        /*        public override void Consume(RangedStat ammo)
                {
                    int numOfProjectileCanProvide = (int)ammo.Value;
                    ActualNumOfProjectile = Mathf.Min(numOfProjectileCanProvide, (int)Mathf.Max(1, 1));
                    ammo.StatValue.BaseValue -= ActualNumOfProjectile;
                }*/
        public class SplitHandler : IHandler
        {
            public float angle;
            private int numberOfProjectile;
            float dmg;
            Quaternion shootDirection;
            public bool IsCompleted => true;

            public SplitHandler(float angle, float dmg, int numberOfProjectile, Quaternion shootDirection)
            {
                this.shootDirection = shootDirection;
                this.dmg = dmg;
                this.angle = angle;
                this.numberOfProjectile = numberOfProjectile;
            }

            void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
            {
                var mostLeftAngle = numberOfProjectile / 2 * angle;
                if (numberOfProjectile % 2 == 0)
                {
                    mostLeftAngle -= angle / 2;
                }
                var mostLeftDirection = shootDirection.Rotate(-mostLeftAngle);

                for (var idx = 0; idx < numberOfProjectile; idx++)
                {
                    var shootDirection = mostLeftDirection.Rotate(idx * angle);
                    var projectile = Instantiate(p, p.transform.position, shootDirection, null);
                    ((ProjectileCollisionHandler)projectile.CollisionHandler).IgnoreCollideEntities = ((ProjectileCollisionHandler)p.CollisionHandler).IgnoreCollideEntities;
                }
            }
        }
    }
}