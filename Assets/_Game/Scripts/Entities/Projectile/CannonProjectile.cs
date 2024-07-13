using System.Collections.Generic;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.GD;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public abstract class CannonProjectile : Projectile, IPhysicsEffectGiver, IGDConfigStatsTarget
    {
        [Header("Cannon Projectile")]
        [Space]
        public string id;
        public GDConfig gDConfig;
        public StatsTemplate statsTemplate;

        public string Id { get => id; set => id = value; }

        public GDConfig GDConfig => gDConfig;

        public StatsTemplate StatsTemplate => statsTemplate;
        protected override void Awake()
        {
            base.Awake();
            GDConfigStatsApplier gDConfigStatsApplier = GetComponent<GDConfigStatsApplier>();
            gDConfigStatsApplier.LoadStats(this);
        }
    }
}