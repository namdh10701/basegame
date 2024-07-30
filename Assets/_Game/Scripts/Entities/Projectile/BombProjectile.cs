using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class BombProjectile : CannonProjectile
    {
        public AreaEffectGiver[] AreaEffectGivers;
        protected override void Awake()
        {
            base.Awake();
            collisionListener.CollisionHandler = new CannonProjectileCollisionHandler(this);
            CannonProjectileCollisionHandler projectileCollisionHandler = (CannonProjectileCollisionHandler)collisionListener.CollisionHandler;
            projectileCollisionHandler.LoopHandlers.Add(new ExplodeHandler(AreaEffectGivers));
        }

        public override void ApplyStats()
        {
            base.ApplyStats();

            foreach (AreaEffectGiver areaEffectGiver in AreaEffectGivers)
            {
                areaEffectGiver.SetRange(_stats.AttackAOE.Value);
                areaEffectGiver.SetDamage(_stats.Damage.Value, _stats.ArmorPenetrate.Value);
            }
        }

        public override void SetDamage(float dmg, bool isCrit)
        {
            base.SetDamage(dmg, isCrit);
            foreach (AreaEffectGiver areaEffectGiver in AreaEffectGivers)
            {
                areaEffectGiver.SetDamage(_stats.Damage.Value, _stats.ArmorPenetrate.Value);
            }
        }

        public class ExplodeHandler : IHandler
        {
            public DamageArea damageArea;
            bool isCompleted;
            public bool IsCompleted => isCompleted;
            AreaEffectGiver[] areaEffectGivers;

            public ExplodeHandler(AreaEffectGiver[] areaEffectGivers)
            {
                this.areaEffectGivers = areaEffectGivers;
            }

            void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
            {

                foreach (AreaEffectGiver areaEffectGiver in areaEffectGivers)
                {
                    AreaEffectGiver spawned = Instantiate(areaEffectGiver, p.transform.position, Quaternion.identity, null);
                    spawned.gameObject.SetActive(true);
                }
                isCompleted = true;
            }
        }


    }
    public class CannonProjectileCollisionHandler : ProjectileCollisionHandler
    {
        public CannonProjectileCollisionHandler(CannonProjectile projectile) : base(projectile)
        {

        }
        public override void FinalAct()
        {
            CannonProjectile cp = this.projectile as CannonProjectile;
            if (cp.IsFever)
            {
                cp.aura.transform.parent = null;
            }
            base.FinalAct();
        }
    }
}