using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities;
using _Game.Scripts;
using UnityEngine;

public class FireballProjectile : CannonProjectile
{
    public DamageArea damageArea;
    public PullEffect pullEffect;
    public ExplosionFx explosionFx;
    protected override void Awake()
    {
        base.Awake();
        ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)collisionListener.CollisionHandler;
        projectileCollisionHandler.LoopHandlers.Add(new ExplodeHandler(damageArea, pullEffect));
    }

    public override void ApplyStats()
    {
        base.ApplyStats();
        explosionFx.SetSize(_stats.AttackAOE.Value);
        damageArea.SetRange(_stats.AttackAOE.Value);
        damageArea.SetDamage(_stats.Damage.Value, _stats.ArmorPenetrate.Value);
        pullEffect.Setsize(_stats.AttackAOE.Value);
    }

    public class ExplodeHandler : IHandler
    {
        public DamageArea damageArea;
        bool isCompleted;
        public bool IsCompleted => isCompleted;
        public PullEffect pullEffect;


        public ExplodeHandler(DamageArea damageArea, PullEffect pullEffect)
        {
            this.damageArea = damageArea;
            this.pullEffect = pullEffect;
        }

        void IHandler.Process(Projectile p, IEffectGiver mainEntity, IEffectTaker collidedEntity)
        {
            isCompleted = true;
            DamageArea da = Instantiate(damageArea, p.transform.position, Quaternion.identity, null);
            da.gameObject.SetActive(true);
            if (pullEffect != null)
                pullEffect.gameObject.SetActive(true);
        }
    }
}
