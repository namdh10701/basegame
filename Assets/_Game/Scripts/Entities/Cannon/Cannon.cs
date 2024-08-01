using System;
using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Features.Gameplay;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PathFinding;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Cannon : Entity, IFighter, IGridItem, IWorkLocation, INodeOccupier, IEffectTaker, IGDConfigStatsTarget, IStunable
    {
        [Header("GD Config Stats Target")]
        public string id;
        public GDConfig gdConfig;
        public StatsTemplate statsTemplate;

        [Header("Cannon")]

        [SerializeField] private GridItemStateManager gridItemStateManager;
        [SerializeField] public GridItemView gridItemView;
        [SerializeField] public CannonFeverStateManager CannonFeverStateManager;

        [SerializeField]
        private CannonStats _stats;

        [SerializeField]
        private CannonStatsTemplate _statsTemplate;
        public IFighterStats FighterStats
        {
            get => _stats;
            set => _stats = (CannonStats)value;
        }

        [field: SerializeReference]
        public AttackStrategy AttackStrategy { get; set; }
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public string GridId { get; set; }
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }

        public EffectHandler effectHandler;
        public EffectHandler EffectHandler { get => effectHandler; }
        public Transform Transform => transform;

        public string Id { get => id; set => id = value; }
        public GDConfig GDConfig => gdConfig;
        public StatsTemplate StatsTemplate => statsTemplate;

        public override Stats Stats => _stats;

        public List<Node> workingSlots = new List<Node>();
        public List<Node> occupyingNodes = new List<Node>();

        public ObjectCollisionDetector FindTargetCollider;
        public FindTargetBehaviour FindTargetBehaviour;

        public CannonAmmo CannonAmmo;
        public override void ApplyStats()
        {
            CannonStats stst = Stats as CannonStats;
            FindTargetCollider.SetRadius(stst.AttackRange.BaseValue);
            AttackStrategy.NumOfProjectile = stst.ProjectileCount;
        }

        #region Controller
        bool isOutOfAmmo;
        bool isStuned;

        public void Initizalize()
        {
            var conf = GameData.CannonTable.FindById(id);
            ConfigLoader.LoadConfig(_stats, conf);
            ApplyStats();

            EffectHandler.EffectTaker = this;


            GridItemStateManager.gridItem = this;
            _stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;

            CannonAmmo.OutOfAmmoStateChaged += OutOfAmmoStateChanged;
            GridItemStateManager.OnStateEntered += OnGridItemStateChanged;
            CannonFeverStateManager.OnStateEntered += OnFeverStateEntered;
            CannonAmmo.Init(this);
            gridItemView.Init(this);
        }

        private void OnFeverStateEntered(CannonFeverState state)
        {
            switch (state)
            {
                case CannonFeverState.None:
                    ApplyNormalStats();
                    break;
                case CannonFeverState.Fever:
                case CannonFeverState.FullFever:
                    ApplyFeverStats();
                    break;
            }
        }



        private void OnGridItemStateChanged(GridItemState state)
        {
            UpdateModel();
        }

        private void OutOfAmmoStateChanged(bool isOutOfAmmo)
        {
            this.isOutOfAmmo = isOutOfAmmo;
            UpdateModel();
        }



        void UpdateModel()
        {
            if (!isOutOfAmmo && gridItemStateManager.GridItemState == GridItemState.Active && !isStuned)
            {
                FindTargetBehaviour.Enable();
            }
            else
            {
                FindTargetBehaviour.Disable();
            }
        }

        private void HealthPoint_OnValueChanged(RangedStat stat)
        {
            if (stat.StatValue.Value <= stat.MinValue)
            {
                gridItemStateManager.GridItemState = GridItemState.Broken;
            }
            else
            {
                gridItemStateManager.GridItemState = GridItemState.Active;
            }
        }

        public void Reload(Ammo bullet, bool isDoubleAmmo)
        {
            CannonAmmo.Reload(bullet, isDoubleAmmo);
        }

        public void OnFeverEffectEnter()
        {
            CannonFeverStateManager.FeverState = CannonFeverState.Fever;
            Invoke("OnFeverEffectExit", 5);

        }
        public void OnFullFeverEffectEnter()
        {
            CannonFeverStateManager.FeverState = CannonFeverState.FullFever;
            Invoke("OnFeverEffectExit", 5);
        }

        public void OnFeverEffectExit()
        {
            CannonFeverStateManager.FeverState = CannonFeverState.None;
        }

        private CannonStatsConfigLoader _configLoader;

        public CannonStatsConfigLoader ConfigLoader
        {
            get
            {
                if (_configLoader == null)
                {
                    _configLoader = new CannonStatsConfigLoader();
                }

                return _configLoader;
            }
        }

        public Stat StatusResist => null;

        public GridItemStateManager GridItemStateManager => gridItemStateManager;

        public Ammo UsingAmmo => CannonAmmo.UsingAmmo;

        public CannonFeverState FeverState => CannonFeverStateManager.FeverState;


        public Action<bool> OnStunStatusChanged;

        public CannonView View;

        void ApplyFeverStats()
        {
            var conf = GameData.CannonFeverTable.FindById(id);
            ConfigLoader.LoadConfig(_stats, conf);
        }

        void ApplyNormalStats()
        {
            var conf = GameData.CannonTable.FindById(id);
            ConfigLoader.LoadConfig(_stats, conf);
        }

        #region Effect status state
        public void OnStun()
        {
            if (CannonFeverStateManager.FeverState != CannonFeverState.None)
                return;
            isStuned = true;
            OnStunStatusChanged.Invoke(isStuned);
        }
        public void OnAfterStun()
        {
            isStuned = false;
            OnStunStatusChanged.Invoke(isStuned);
        }
        #endregion

        #region Grid Item State
        public void Active()
        {
            UpdateModel();
        }
        public void OnBroken()
        {
            GlobalEvent<Cannon, int>.Send("FixCannon", this, CrewJobData.DefaultPiority[typeof(FixCannonTask)]);
        }
        #endregion

        #endregion
    }
}