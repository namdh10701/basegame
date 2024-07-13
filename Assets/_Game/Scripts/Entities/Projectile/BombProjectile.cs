using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts;
using UnityEngine;

public class BombProjectile : CannonProjectile
{
    public AreaEffectGiver[] AreaEffectGivers;
    public ExplosionFx explosionFx;
    protected override void Awake()
    {
        base.Awake();
        ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)collisionListener.CollisionHandler;
        projectileCollisionHandler.LoopHandlers.Add(new ExplodeHandler(AreaEffectGivers));
    }

    public override void ApplyStats()
    {
        base.ApplyStats();
        explosionFx.SetSize(_stats.AttackAOE.Value);
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
