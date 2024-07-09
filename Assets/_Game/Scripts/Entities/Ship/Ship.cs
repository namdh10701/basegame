using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Game.Features.Battle;
using _Game.Scripts;
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using DG.Tweening;
using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

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

        public PathfindingController PathfindingController;
        public EffectTakerCollider EffectCollider;
        public Area ShipArea;
        public ShipSpeed ShipSpeed;
        public CrewJobData CrewJobData;
        public BattleViewModel BattleViewModel;
        public FeverModel FeverModel;
        public ShipHUD HUD;

        private void Start()
        {
            GlobalEvent<EnemyModel>.Register("EnemyDied", OnEnemyDied);
            GlobalEvent.Register("UseFever", UseFever);
            GlobalEvent<Cannon>.Register("ClickCannon", ShowShipHUD);
            GlobalEvent.Register("CloseHUD", CloseHUD);
            GetComponent<GDConfigStatsApplier>().LoadStats(this);
            FeverModel.SetFeverPointStats(stats.Fever);
            PathfindingController.Initialize();
            ShipSetup.Initialize();
            CrewJobData.Initialize();
            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                cannon.HUD.RegisterJob(CrewJobData);
            }
            foreach (Ammo ammo in ShipSetup.Ammos)
            {
                ammo.HUD.RegisterJob(CrewJobData);
            }
            HUD.Initialize(ShipSetup.Ammos);
            BattleViewModel = GameObject.Find("BattleScreen(Clone)").GetComponent<BattleViewModel>();
            BattleViewModel.FeverView.Init(FeverModel);
        }

        public void UseFever()
        {
            FeverModel.OnUseFever();
            BattleManager.Instance.FeverSpeedFx.Activate();
            DOTween.To(() => stats.Fever.StatValue.BaseValue, x => stats.Fever.StatValue.BaseValue = x, 0, 10).OnComplete(
                () =>
                {
                    BattleManager.Instance.FeverSpeedFx.Deactivate();
                    FeverModel.UpdateState();
                });
        }

        public void UseWeakFever()
        {
            stats.Fever.StatValue.BaseValue -= 200;
            stats.Fever.StatValue.BaseValue = Mathf.Clamp(stats.Fever.StatValue.BaseValue, 0, stats.Fever.MaxStatValue.BaseValue);
            FeverModel.UpdateState();
        }

        private void OnDestroy()
        {
            GlobalEvent<EnemyModel>.Unregister("EnemyDied", OnEnemyDied);
            GlobalEvent.Unregister("UseFever", UseFever);
        }

        public void OnEnemyDied(EnemyModel enemyModel)
        {
            if (FeverModel.CurrentState != FeverState.Unleashing)
            {

                float feverPointGained = ((EnemyStats)enemyModel.Stats).FeverPoint.Value;
                AddFeverPoint(feverPointGained);

            }
        }

        void AddFeverPoint(float point)
        {
            stats.Fever.StatValue.BaseValue += point;
            stats.Fever.StatValue.BaseValue = Mathf.Clamp(stats.Fever.StatValue.BaseValue, 0, stats.Fever.MaxStatValue.BaseValue);
            FeverModel.UpdateState();
        }

        private void Update()
        {
            RegenMP();
            UpdateBattleView();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddFeverPoint(50);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                UseWeakFever();
            }
        }

        void UpdateBattleView()
        {
            if (BattleViewModel != null)
            {
                BattleViewModel.HP = stats.HealthPoint.Value;
                BattleViewModel.MaxHP = stats.HealthPoint.MaxValue;
                BattleViewModel.MP = stats.ManaPoint.Value;
                BattleViewModel.MaxMP = stats.ManaPoint.MaxValue;
                BattleViewModel.Fever = stats.Fever.Value;
            }
        }

        void RegenMP()
        {
            if (!stats.ManaPoint.IsFull)
            {
                stats.ManaPoint.StatValue.BaseValue += (stats.ManaRegenerationRate.Value * Time.deltaTime);
            }
        }

        public override void ApplyStats()
        {

        }
        private void ShowShipHUD(Cannon cannon)
        {
            Debug.Log(cannon);
            if (cannon == null)
            {
                HUD.Hide();
                return;
            }
            HUD.Cannon = cannon;
            HUD.Show();
        }

        private void CloseHUD()
        {
            HUD.Hide();
        }
    }
}
