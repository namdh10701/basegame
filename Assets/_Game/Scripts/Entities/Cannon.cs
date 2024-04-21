using System;
using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.AimTarget;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Behaviours.AttackStrategies;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Game.Scripts.Entities
{
    public class Cannon: Entity, IShooter
    {
        
        [ContextMenu("TestMyMethod")]
        void TestMyMethod()
        {
            RangedStat stat = new RangedStat(10, 0, 100);
            
            
            Assert.AreEqual(10, stat.Value);
            stat.StatValue.AddModifier(StatModifier.Flat(10));
            
            Debug.Log("stat ok 1");
            // Assert.AreEqual(20, stat.Value);
            //
            stat.StatValue.AddModifier(StatModifier.Flat(100));
            // Assert.AreEqual(100, stat.Value);
            
            Debug.Log("stat ok " + stat.StatValue.Value);

        }
        [SerializeField]
        private CannonStats _stats;

        public override Stats Stats => _stats;
        public IFighterStats FighterStats
        {
            get => _stats;
            set => _stats = (CannonStats)value;
        }

        // public IAttackStrategy AttackStrategy { get; set; } = new ShootTargetStrategy_Normal();
        [field:SerializeReference]
        public AttackStrategy AttackStrategy { get; set; }// = new ShootTargetStrategyNormal_SplitShot();

        [field:SerializeField]
        public List<Effect> BulletEffects { get; set; } = new ();

        // public ShootTargetTriggerBehaviour ShootTargetTriggerBehaviour;
        // public AimTargetBehaviour aimTargetBehaviour;

        private void Start()
        {
            var eff = gameObject.AddComponent<DecreaseHealthEffect>();
            eff.Amount = _stats.AttackDamage.Value;
            BulletEffects.Add(eff);
        }
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