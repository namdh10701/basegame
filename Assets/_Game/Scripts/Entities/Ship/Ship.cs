using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Features.Battle;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using UnityEngine;

namespace _Game.Features.Gameplay
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

        public Stat StatusResist => null;

        public PathfindingController PathfindingController;
        public EffectTakerCollider EffectCollider;
        public Area ShipArea;
        public Area ShipBound;
        public ShipSpeed ShipSpeed;
        public CrewJobData CrewJobData;
        public FeverModel FeverModel;
        public ShipHUD HUD;
        private void Awake()
        {
            EffectCollider.Taker = this;
            EffectHandler.EffectTaker = this;
            Initialize();
        }
        private ShipStatsConfigLoader _configLoader;

        public ShipStatsConfigLoader ConfigLoader
        {
            get
            {
                if (_configLoader == null)
                {
                    _configLoader = new ShipStatsConfigLoader();
                }

                return _configLoader;
            }
        }

        public void Initialize()
        {
            var conf = GameData.ShipTable.FindById(id);
            ConfigLoader.LoadConfig(stats, conf);
            ApplyStats();


            GlobalEvent<EnemyStats>.Register("EnemyDied", OnEnemyDied);
            FeverModel.SetFeverPointStats(stats.Fever);
            PathfindingController.Initialize();
            ShipSetup.Initialize();
            CrewJobData.Initialize();
            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                cannon.View.cannonHUD.RegisterJob(CrewJobData);
            }
            reloadCannonController.Init(this);
            HUD.Initialize(ShipSetup.Ammos);
            stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
        }
        bool isDead;
        private void HealthPoint_OnValueChanged(RangedStat obj)
        {
            // if (obj.Value <= obj.MinValue)
            {
                if (!isDead)
                {
                    isDead = true;
                    ShipSetup.HideHUD();
                    reloadCannonController.enabled = false;
                    ShipSetup.DisableAllItem();
                    BattleManager.Instance.Lose();
                }
            }
        }

        public ReloadCannonController reloadCannonController;

        public void EnterFullFever()
        {
            FeverModel.OnUseFever();
            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                cannon.OnFullFeverEffectEnter();
            }

        }
        public void ExitFullFever()
        {
            FeverModel.CurrentState = FeverState.State0;
            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                cannon.OnFeverEffectExit();
            }
        }

        private void OnDestroy()
        {
            GlobalEvent<EnemyStats>.Unregister("EnemyDied", OnEnemyDied);
        }

        public void OnEnemyDied(EnemyStats enemyModel)
        {
            if (FeverModel.CurrentState != FeverState.Unleashing)
            {
                Debug.Log("Add Fever Point");
                float feverPointGained = enemyModel.FeverPoint.Value;
                AddFeverPoint(feverPointGained);

            }
        }

        void AddFeverPoint(float point)
        {
            stats.Fever.StatValue.BaseValue += point;
        }


        private void Update()
        {
            RegenMP();
            RegenHP();
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

        public override void ApplyStats()
        {

        }

        public void HideHUD()
        {
            ShipSetup.HideHUD();
        }
    }
}
