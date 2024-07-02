using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Features.Battle;
using _Game.Scripts.Battle;
using _Game.Scripts.GD;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity, IEffectTaker
    {
        [Space]
        [Header("Ship")]
        public ShipStats stats;
        public ShipSetup ShipSetup;
        public override Stats Stats => stats;
        public ShipStatsTemplate _statTemplate;
        public EffectHandler effectHandler;

        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;
        public PathfindingController PathfindingController;
        public EffectTakerCollider EffectCollider;
        public Area ShipArea;
        public ShipSpeed ShipSpeed;
        public CrewJobData CrewJobData;
        public BattleViewModel BattleViewModel;
        protected override void Awake()
        {
            base.Awake();
            EffectCollider.Taker = this;
            PathfindingController.Initialize();
            ShipSetup.Initialize();
            CrewJobData.Initialize();
        }
        private void Start()
        {
            BattleViewModel = GameObject.Find("BattleScreen(Clone)").GetComponent<BattleViewModel>();

        }
        protected override void LoadStats()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Ships.TryGetValue(Id, out ShipConfig shipConfig))
                {
                    shipConfig.ApplyGDConfig(stats);
                }
            }
            else
            {
                _statTemplate.ApplyConfig(stats);
            }
        }

        protected override void LoadModifiers()
        {

        }

        protected override void ApplyStats()
        {
            foreach (Cell cell in ShipSetup.AllCells)
            {
                cell.stats.HealthPoint.MaxStatValue.BaseValue = stats.HealthPoint.MaxStatValue.Value / ShipSetup.AllCells.Count;
                cell.stats.HealthPoint.StatValue.BaseValue = cell.stats.HealthPoint.MaxStatValue.BaseValue;
            }
        }


        private void Update()
        {
            RegenMP();
            //RegenHP();
            UpdateBattleView();
        }

        void UpdateBattleView()
        {
            if (BattleViewModel != null)
            {
                BattleViewModel.HP = stats.HealthPoint.Value;
                BattleViewModel.MaxHP = stats.HealthPoint.MaxValue;
                BattleViewModel.MP = stats.ManaPoint.Value;
                BattleViewModel.MaxMP = stats.ManaPoint.MaxValue;
            }
        }

        void RegenMP()
        {
            if (!stats.ManaPoint.IsFull)
            {
                stats.ManaPoint.StatValue.BaseValue += (stats.ManaRegenerationRate.Value * Time.deltaTime);
            }
        }

        void RegenHP()
        {
            if (!stats.HealthPoint.IsFull)
            {
                stats.HealthPoint.StatValue.BaseValue += (stats.HealthRegenerationRate.Value * Time.deltaTime);
            }
        }

    }
}
