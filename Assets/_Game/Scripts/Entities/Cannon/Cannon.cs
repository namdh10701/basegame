using System.Collections.Generic;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities.CannonComponent;
using _Game.Scripts.GD;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Game.Scripts.Entities
{
    public class Cannon : Entity, IGridItem, IShooter
    {

        [ContextMenu("Apply stats changed")]
        void ApplyStatsChanged()
        {
            // _stats.AttackDamage.TriggerValueChanged();
            foreach (var propertyInfo in _stats.GetType().GetProperties())
            {
                propertyInfo.PropertyType.GetMethod("TriggerValueChanged").Invoke(propertyInfo, new object[] { });
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

        protected override void Awake()
        {
            base.Awake();
        }

        [Header("Cannon")]
        [field: SerializeField]
        private GridItemDef def;

        [SerializeField]
        private CannonStats _stats;

        [SerializeField]
        private CannonStatsTemplate _statsTemplate;

        [SerializeField]
        private CannonReloader _cannonReloader;

        public CannonReloader Reloader => _cannonReloader;

        public override Stats Stats => _stats;

        public IFighterStats FighterStats
        {
            get => _stats;
            set => _stats = (CannonStats)value;
        }

        // public IAttackStrategy AttackStrategy { get; set; } = new ShootTargetStrategy_Normal();
        [field: SerializeReference]
        public AttackStrategy AttackStrategy { get; set; }// = new ShootTargetStrategyNormal_SplitShot();

        [field: SerializeField]
        public List<Effect> BulletEffects { get; set; } = new();

        public Transform behaviour;
        public List<Vector2Int> OccupyCells { get; set; }
        public GridItemDef Def { get => def; set => def = value; }
        public Transform Behaviour { get => behaviour; }
        public string GridId { get; set; }

        private void Start()
        {
            LoadShipStats();
            LoadModifiers();
        }
        void LoadShipStats()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Cannons.TryGetValue(def.Id, out CannonConfig cannonConfig))
                {
                    ApplyConfig(cannonConfig);
                }
            }
            else
            {
                _stats = ResourceLoader.LoadCannonStatsTemplate(def.Id).Data;
            }

        }

        void ApplyConfig(CannonConfig shipConfig)
        {

        }

        void LoadModifiers()
        {

        }
    }
}