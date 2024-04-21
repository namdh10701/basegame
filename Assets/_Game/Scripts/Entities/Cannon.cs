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
        
        // [SerializeField]
        // private int _score;
        //
        // public int Score
        // {
        //     get => _score;
        //     set
        //     {
        //         _score = value;
        //         Debug.Log("aaaaaaaaaaaaaaaaaaaaaa");
        //     }
        // }
        
        [ContextMenu("Apply stats changed")]
        void ApplyStatsChanged()
        {
            // _stats.AttackDamage.TriggerValueChanged();
            foreach (var propertyInfo in _stats.GetType().GetProperties())
            {
                propertyInfo.PropertyType.GetMethod("TriggerValueChanged").Invoke(propertyInfo, new object[]{});
            }
        }
        
        [ContextMenu("Save stats")]
        void SaveStats()
        {
            _statsTemplate.Data = _stats;
        }
        
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
        
        [SerializeField]
        private CannonStatsTemplate _statsTemplate;

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
            // _stats = Instantiate(_statsTemplate).Data;
            // var eff = gameObject.AddComponent<DecreaseHealthEffect>();
            // eff.Amount = _stats.AttackDamage.Value;
            // BulletEffects.Add(eff);
            //
            // _stats.AttackDamage.OnValueChanged += stat =>
            // {
            //     eff.Amount = stat.Value;
            // };
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