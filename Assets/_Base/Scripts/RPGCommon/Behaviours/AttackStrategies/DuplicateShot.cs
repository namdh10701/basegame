using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Base.Scripts.Utils.Extensions;
using System.Collections.Generic;
using UnityEngine;
using static _Base.Scripts.RPGCommon.Behaviours.AttackStrategies.BouncingShot;

namespace _Base.Scripts.RPGCommon.Behaviours.AttackStrategies
{
    [AddComponentMenu("[Attack Strategy] Duplicate Shot")]
    public class DuplicateShot : NormalShot
    {
        public float angle = 15f;
        public int amount = 3;
        public override void DoAttack()
        {
            var shootDirection = CalculateShootDirection();
            var projectile = SpawnProjectile(shootDirection, shootPosition);
            ((ProjectileCollisionHandler)projectile.CollisionHandler).Handlers.Add(new DupplicateHandler(angle, amount));
        }

        public class DupplicateHandler : IHandler
        {
            float angle;
            float amount;
            bool isActivated;
            public bool IsCompleted => isActivated;

            public DupplicateHandler(float angle, float amount)
            {
                this.angle = angle;
                this.amount = amount;
            }

            void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
            {
                isActivated = true;
                var centerDirection = p.transform.rotation;

                var mostLeftAngle = amount / 2 * angle;
                if (amount % 2 == 0)
                {
                    mostLeftAngle -= angle / 2;
                }
                var mostLeftDirection = centerDirection.Rotate(-mostLeftAngle);
                for (var idx = 0; idx < amount; idx++)
                {
                    var shootDirection = mostLeftDirection.Rotate(idx * angle);
                    var projectileEntity = Object.Instantiate(p.gameObject, p.transform.position, shootDirection, null);
                    var projectile = projectileEntity.GetComponent<Projectile>();
                    ((ProjectileCollisionHandler)projectile.CollisionHandler).IgnoreCollideEntities.Add(collidedEntity);
                }
            }
        }
    }
}