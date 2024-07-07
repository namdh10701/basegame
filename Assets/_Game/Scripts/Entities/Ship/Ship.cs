using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Features.Battle;
using _Game.Scripts.Battle;
using _Game.Scripts.GD;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity, IEffectTaker, IStatsBearer, IGDConfigStatsTarget
    {
        [Header("GD Config Stats Target")]
        [SerializeField] private string id;
        [SerializeField] private GDConfig gdConfig;
        [SerializeField] private StatsTemplate statsTemplate;

        [Space]
        [Header("Ship")]
        public ShipStats stats;
        public ShipSetup ShipSetup;
        public string Id { get => id; set => id = value; }

        public GDConfig GDConfig => gdConfig;

        public StatsTemplate StatsTemplate => statsTemplate;

        public override Stats Stats => stats;


        public EffectHandler effectHandler;


        public Transform Transform => transform;

        public EffectHandler EffectHandler => effectHandler;

        public PathfindingController PathfindingController;
        public EffectTakerCollider EffectCollider;
        public Area ShipArea;
        public ShipSpeed ShipSpeed;
        public CrewJobData CrewJobData;
        public BattleViewModel BattleViewModel;
        protected void Awake()
        {
            PathfindingController.Initialize();
            ShipSetup.Initialize();
            CrewJobData.Initialize();
        }
        private void Start()
        {
            //BattleViewModel = GameObject.Find("BattleScreen(Clone)").GetComponent<BattleViewModel>();

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

        void IStatsBearer.ApplyStats()
        {
           
        }
    }
}
