using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts;
using UnityEngine;

public class FireballProjectile : CannonProjectile
{
    public DamageArea damageArea;
    public ExplosionFx explosionFx;
    protected override void Awake()
    {
        base.Awake();
        ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)collisionListener.CollisionHandler;
        projectileCollisionHandler.LoopHandlers.Add(new ExplodeHandler(damageArea));
    }

    protected override void Start()
    {
        damageArea.SetDamage(_stats.Damage.Value, _stats.ArmorPenetrate.Value);
    }

    protected override void ApplyStats()
    {
        base.ApplyStats();
        explosionFx.SetSize(_stats.AttackAOE.Value);
        damageArea.SetRange(_stats.AttackAOE.Value);
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
