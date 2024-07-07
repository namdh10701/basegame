using System.Collections.Generic;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class CannonProjectile : Projectile, IPhysicsEffectGiver, IGDConfigStatsTarget
    {
        [Header("Cannon Projectile")]
        [Space]
        public string id;
        public GDConfig gDConfig;
        public StatsTemplate statsTemplate;

        public string Id { get => id; set => id = value; }

        public GDConfig GDConfig => gDConfig;

        public StatsTemplate StatsTemplate => statsTemplate;

        public override void ApplyStats()
        {
            base.ApplyStats();
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