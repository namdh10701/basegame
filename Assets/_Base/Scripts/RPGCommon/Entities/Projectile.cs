using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using UnityEngine;

namespace _Base.Scripts.RPGCommon.Entities
{
    public class Projectile : Entity
    {
        public Rigidbody2D body;
        public ParticleSystem onHitParticle;
        [SerializeField]
        private ProjectileStats projectileStats;
        public ProjectileMovement ProjectileMovement;
        public override Stats Stats => projectileStats;
        protected override void Awake()
        {
            base.Awake();
            CollisionHandler = new ProjectileCollisionHandler(this);
            ProjectileCollisionHandler projectileCollisionHandler = (ProjectileCollisionHandler)CollisionHandler;
            if (onHitParticle != null)
            {
                projectileCollisionHandler.LoopHandlers.Add(new ParticleHandler(onHitParticle));
            }
            projectileCollisionHandler.Handlers.Add(new PiercingHandler((int)projectileStats.Piercing.Value));
            OutgoingEffects = new System.Collections.Generic.List<Effect>() { new DecreaseHealthEffect(2) };
            ProjectileMovement = new StraightMove(this);
        }
        private void FixedUpdate()
        {
            ProjectileMovement.Move();
        }

        public void AddMoveSpeed(StatModifier statModifier)
        {
            projectileStats.Speed.AddModifier(statModifier);
        }

        public void AddDamage(StatModifier statModifier)
        {
            projectileStats.Damage.AddModifier(statModifier);
        }

        public void AddCritDamage(StatModifier statModifier)
        {
            projectileStats.CritDamage.AddModifier(statModifier);
        }

        public void AddCritChance(StatModifier statModifier)
        {
            projectileStats.CritChance.AddModifier(statModifier);
        }


        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }

}