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
        public Area ShipBound;
        public ShipSpeed ShipSpeed;
        public CrewJobData CrewJobData;
        public BattleViewModel BattleViewModel;
        public FeverModel FeverModel;
        public ShipHUD HUD;

      

        public void Initialize(BattleViewModel battleViewModel)
        {
            GlobalEvent<EnemyModel>.Register("EnemyDied", OnEnemyDied);
            //GlobalEvent<Cannon>.Register("CLICK_CANNON", ShowShipHUD);
            //GlobalEvent.Register("CloseHUD", CloseHUD);
            GetComponent<GDConfigStatsApplier>().LoadStats(this);

            if (EnemyManager.floorId == "1")
            {
                stats.Fever.StatValue.BaseValue = 0;
            }
            else
            {
                float fever = PlayerPrefs.GetFloat("fever", 0);
                stats.Fever.StatValue.BaseValue = fever;
            }
            EffectCollider.Taker = this;
            EffectHandler.EffectTaker = this;
            FeverModel.SetFeverPointStats(stats.Fever);
            PathfindingController.Initialize();
            ShipSetup.Initialize();
            CrewJobData.Initialize();
            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                cannon.HUD.RegisterJob(CrewJobData);
            }
            HUD.Initialize(ShipSetup.Ammos);
            this.BattleViewModel = battleViewModel;
            BattleViewModel.FeverView.Init(FeverModel);
        }

        public void UseFullFever()
        {
            FeverModel.OnUseFever();
            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                //CrewJobData.ReloadCannonJobsDic[cannon].Status = JobStatus.Deactive;
                cannon.OnFullFeverEffectEnter();
            }
            BattleManager.Instance.FeverSpeedFx.Activate();
            DOTween.To(() => stats.Fever.StatValue.BaseValue, x => stats.Fever.StatValue.BaseValue = x, 0, 10).OnComplete(
                () =>
                {
                    BattleManager.Instance.FeverSpeedFx.Deactivate();
                    FeverModel.UpdateState();
                    foreach (Cannon cannon in ShipSetup.Cannons)
                    {
                        cannon.OnFullFeverEffectExit();
                    }

                });

        }
        public void UseFever(Cannon cannon)
        {
            //CrewJobData.ReloadCannonJobsDic[cannon].Status = JobStatus.Deactive;
            stats.Fever.StatValue.BaseValue -= 200;
            stats.Fever.StatValue.BaseValue = Mathf.Clamp(stats.Fever.StatValue.BaseValue, 0, stats.Fever.MaxStatValue.BaseValue);
            FeverModel.UpdateState();
            cannon.OnFeverEffectEnter();
        }

        private void OnDestroy()
        {
            GlobalEvent<EnemyModel>.Unregister("EnemyDied", OnEnemyDied);
            /*GlobalEvent<Cannon>.Unregister("ClickCannon", ShowShipHUD);
            GlobalEvent.Unregister("CloseHUD", CloseHUD);*/
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

        public void HideHUD()
        {
            ShipSetup.HideHUD();
        }
    }
}
