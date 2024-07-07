using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts;
using UnityEngine;

public class FireballProjectile : CannonProjectile
{
    public DamageArea damageArea;
    public ExplosionFx explosionFx;
    protected void Awake()
    {
        ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)collisionListener.CollisionHandler;
        projectileCollisionHandler.LoopHandlers.Add(new ExplodeHandler(damageArea));
    }

    public override void ApplyStats()
    {
        base.ApplyStats();
        explosionFx.SetSize(_stats.AttackAOE.Value);
        damageArea.SetRange(_stats.AttackAOE.Value);
        damageArea.SetDamage(_stats.Damage.Value, _stats.ArmorPenetrate.Value);
    }

    public class ExplodeHandler : IHandler
    {
        public DamageArea damageArea;
        bool isCompleted;
        public bool IsCompleted => isCompleted;

        public ExplodeHandler(DamageArea damageArea)
        {
            this.damageArea = damageArea;
        }

        void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
        {
            isCompleted = true;
            DamageArea da = Instantiate(damageArea, p.transform.position, Quaternion.identity, null);
            da.gameObject.SetActive(true);
        }
    }
}
