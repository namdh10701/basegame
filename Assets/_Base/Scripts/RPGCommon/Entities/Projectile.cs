using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.GD;
using _Game.Scripts.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public abstract class Projectile : Entity, IUpgradeable, IEffectGiver
    {
        [Header("Projectile")]
        public ParticleSystem onHitParticle;
        public ProjectileStats _stats;
        public ProjectileStatsTemplate _statsTemplate;
        public ProjectileMovement ProjectileMovement;
        public Transform trail;


        public override Stats Stats => _stats;

        public Rarity rarity;
        public Rarity Rarity { get => rarity; set => rarity = value; }

        public Transform Transform => transform;

        public List<Effect> outGoingEffects = new List<Effect>();

        public List<Effect> OutGoingEffects { get => outGoingEffects; set => outGoingEffects = value; }

        public EffectGiverCollisionListener collisionListener;

        public EffectCollisionHandler CollisionHandler;

        public bool isCrit;

        protected override void Awake()
        {
            base.Awake();
            CollisionHandler = new ProjectileCollisionHandler(this);
            collisionListener.CollisionHandler = CollisionHandler;
            ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)collisionListener.CollisionHandler;
            if (onHitParticle != null)
            {
                projectileCollisionHandler.LoopHandlers.Add(new ParticleHandler(onHitParticle));
            }
            projectileCollisionHandler.Handlers.Add(new PiercingHandler((int)_stats.Piercing.Value));
            ProjectileMovement = new StraightMove(this);
        }

        private void FixedUpdate()
        {
            ProjectileMovement.Move();
        }

        public void AddMoveSpeed(StatModifier statModifier)
        {
            _stats.Speed.AddModifier(statModifier);
        }

        public void AddDamage(StatModifier statModifier)
        {
            _stats.Damage.AddModifier(statModifier);
        }

        public void AddCritDamage(StatModifier statModifier)
        {
            _stats.CritDamage.AddModifier(statModifier);
        }

        public void AddCritChance(StatModifier statModifier)
        {
            _stats.CritChance.AddModifier(statModifier);
        }

        public void AddAccuaracy(StatModifier statModifier)
        {
            _stats.Accuracy.AddModifier(statModifier);
        }

        public void SetDamage(float dmg, bool isCrit)
        {
            _stats.Damage.BaseValue = dmg;
            this.isCrit = isCrit;
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }

}