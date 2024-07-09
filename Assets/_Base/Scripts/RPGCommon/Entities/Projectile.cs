using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public abstract class Projectile : Entity, IEffectGiver
    {
        [Header("Projectile")]
        public Rigidbody2D body;
        public ParticleSystem onHitParticle;
        public ProjectileStats _stats;
        public ProjectileMovement ProjectileMovement;
        public Transform trail;
        public override Stats Stats => _stats;

        public Transform Transform => transform;

        [SerializeField] protected List<Effect> outGoingEffects = new List<Effect>();

        public List<Effect> OutGoingEffects { get => outGoingEffects; set => outGoingEffects = value; }

        public EffectGiverCollisionListener collisionListener;

        public EffectCollisionHandler CollisionHandler;

        public bool isCrit;

        protected virtual void Awake()
        {
            CollisionHandler = new ProjectileCollisionHandler(this);
            collisionListener.CollisionHandler = CollisionHandler;

        }
        public override void ApplyStats()
        {
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
        private void OnDestroy()
        {
            foreach (Effect effect in outGoingEffects)
            {
                if (effect != null && !effect.IsActive)
                {
                    Destroy(effect.gameObject);
                }
            }
        }
    }

}