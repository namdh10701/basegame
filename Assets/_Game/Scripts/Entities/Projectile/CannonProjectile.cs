using System.Collections.Generic;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;

namespace _Game.Scripts.Entities
{
    public class CannonProjectile : Projectile, IPhysicsEffectGiver, IGDConfigStatsTarget
    {
        public string id;
        protected virtual void Start()
        {
            DecreaseHealthEffect decreaseHpEffect = gameObject.AddComponent<DecreaseHealthEffect>();
            decreaseHpEffect.Amount = _stats.Damage.Value;
            decreaseHpEffect.AmmoPenetrate = _stats.Damage.Value;
            decreaseHpEffect.IsCrit = isCrit;
            PushEffect pushEffect = gameObject.AddComponent<PushEffect>();
            pushEffect.force = 150;
            pushEffect.body = body;
            outGoingEffects = new List<Effect>() {
                decreaseHpEffect,
                pushEffect
        };
        }
    }
}