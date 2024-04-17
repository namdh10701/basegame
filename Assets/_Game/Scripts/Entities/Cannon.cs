using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Behaviours.AttackStrategies;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Cannon: Entity, IShooter
    {
        [SerializeField]
        private CannonStats _stats = new CannonStats();

        public override Stats Stats => _stats;
        public IFighterStats FighterStats
        {
            get => _stats;
            set => _stats = (CannonStats)value;
        }

        // public IAttackStrategy AttackStrategy { get; set; } = new ShootTargetStrategy_Normal();
        [field:SerializeReference]
        public AttackStrategy AttackStrategy { get; set; }// = new ShootTargetStrategyNormal_SplitShot();

        public List<Effect> BulletEffects { get; set; } = new ();

        // public ShootTargetTriggerBehaviour ShootTargetTriggerBehaviour;
        // public AimTargetBehaviour aimTargetBehaviour;

        // protected override void Awake()
        // {
        //     base.Awake();
        //     InvokeRepeating("AutoAttack", 0f, 1f);
        // }
        //
        // protected void AutoAttack()
        // {
        //     if (!aimTargetBehaviour.IsReadyToAttack)
        //     {
        //         return;
        //     }
        //     
        //     AttackStrategy.SetData(this, shootPosition, projectilePrefab, aimTargetBehaviour.FollowTargetBehaviour.FindTargetBehaviour.Strategy, aimTargetBehaviour.LockedPosition);
        // }
    }
}