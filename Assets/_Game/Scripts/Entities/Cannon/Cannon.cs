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
        [field: SerializeField]
        private GridItemDef def;

        [SerializeField] private GridItemStateManager gridItemStateManager;


        [SerializeField]
        private CannonStats _stats;

        [SerializeField]
        private CannonStatsTemplate _statsTemplate;

        public GameObject border;
        public IFighterStats FighterStats
        {
            get => _stats;
            set => _stats = (CannonStats)value;
        }

        [field: SerializeReference]
        public AttackStrategy AttackStrategy { get; set; }
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; set => def = value; }
        public string GridId { get; set; }
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }
        public bool IsBroken { get => isBroken; set => isBroken = value; }

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
        public AttackTargetBehaviour AttackTargetBehaviour;
        public FindTargetBehaviour FindTargetBehaviour;

        public Ammo usingBullet;
        public SpineAnimationCannonHandler Animation;
        public GDConfigStatsApplier GDConfigStatsApplier;
        public override void ApplyStats()
        {
            CannonStats stst = Stats as CannonStats;
            FindTargetCollider.SetRadius(stst.AttackRange.BaseValue);
            AttackStrategy.NumOfProjectile = stst.ProjectileCount;
        }

        #region Controller

        public CannonHUD HUD;
        bool isBroken;
        bool isOutOfAmmo;
        bool isStuned;

        public void Initizalize()
        {
            EffectHandler.EffectTaker = this;
            GDConfigStatsApplier = GetComponent<GDConfigStatsApplier>();
            GDConfigStatsApplier.LoadStats(this);
            border.SetActive(false);
            _stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            _stats.Ammo.OnValueChanged += Ammo_OnValueChanged;

            HUD.SetCannon(this);
        }

        private void Ammo_OnValueChanged(RangedStat stat)
        {

            if (stat.StatValue.Value == stat.MinValue)
            {
                OnOutOfAmmo();
            }
            else if (stat.StatValue.Value > 0)
            {
                OnReloaded();
            }
        }

        private void HealthPoint_OnValueChanged(RangedStat stat)
        {
            if (stat.StatValue.Value <= stat.MinValue)
            {
                OnBroken();
            }
        }

        public void OnOutOfAmmo()
        {
            if (usingBullet.AmmoType == AmmoType.Bomb && isOnFever)
            {
                //OnFeverEffectExit();
            }
            isOutOfAmmo = true;
            GlobalEvent<Cannon, Ammo, int>.Send("Reload", this, usingBullet, 15);
            OutOfAmmo?.Invoke();
            UpdateVisual();
            FindTargetBehaviour.Disable();
        }
        public void OnBroken()
        {
            Debug.Log("SEND");
            isBroken = true;
            GlobalEvent<Cannon, int>.Send("FixCannon", this, CrewJobData.DefaultPiority[typeof(FixCannonTask)]);
            Broken?.Invoke();
            UpdateVisual();

            FindTargetBehaviour.Disable();
        }

        public Action OutOfAmmo;
        public Action Broken;

        public void OnReloaded()
        {
            isOutOfAmmo = false;
            UpdateVisual();
        }
        void UpdateVisual()
        {
            if (!isOutOfAmmo && !isBroken && !isStuned)
            {
                FindTargetBehaviour.Enable();
                Animation.PlayNormal();
            }
            else
            {
                Animation.PlayBroken();
            }
        }

        public void OnFixed()
        {
            isBroken = false;
            _stats.HealthPoint.StatValue.BaseValue = _stats.HealthPoint.MaxStatValue.Value / 100 * 30;
            UpdateVisual();
        }

        public void Reload(Ammo bullet)
        {
            if (bullet == null)
            {

                _stats.Ammo.MaxStatValue.BaseValue = 1;
                _stats.Ammo.StatValue.BaseValue = 0;
                OnOutOfAmmo();

            }
            else
            {
                usingBullet = bullet;
                AmmoStats ammoStats = (AmmoStats)bullet.Stats;

                _stats.Ammo.MaxStatValue.BaseValue = ammoStats.MagazineSize.BaseValue;
                _stats.Ammo.StatValue.BaseValue = ammoStats.MagazineSize.BaseValue;
                AttackTargetBehaviour.projectilePrefab = bullet.Projectile;
            }
        }

        public bool IsOnFever => isOnFever;
        public bool IsOnFullFever => isOnFullFever;

        public ParticleSystem feverFx;
        public ParticleSystem feverEnterFx;

        bool isOnFever;
        bool isOnFullFever;

        public Action OnFeverStart;
        public Action OnFeverEnded;

        public void OnFeverEffectEnter()
        {
            isOnFever = true;
            feverEnterFx.Play();
            if (feverFx != null)
            {
                feverFx.Play();
            }
            OnFeverStart?.Invoke();
            ApplyFeverStats();
            // code change stats go here 
            if (usingBullet.AmmoType == AmmoType.Bomb)
            {
                _stats.Ammo.StatValue.BaseValue = usingBullet.stats.MagazineSize.BaseValue + 5;
            }
            else
            {
                _stats.Ammo.StatValue.BaseValue = usingBullet.stats.MagazineSize.BaseValue;
            }
            Invoke("OnFeverEffectExit", 5);

        }
        public void OnFullFeverEffectEnter()
        {
            feverEnterFx.Play();
            if (feverFx != null)
            {
                feverFx.Play();
            }
            isOnFullFever = true;
            ApplyFeverStats();
            if (usingBullet.AmmoType == AmmoType.Bomb)
            {
                _stats.Ammo.StatValue.BaseValue = usingBullet.stats.MagazineSize.BaseValue + 10;
            }
            else
            {
                _stats.Ammo.StatValue.BaseValue = usingBullet.stats.MagazineSize.BaseValue;
            }
        }

        public void OnFullFeverEffectExit()
        {

            if (!isOnFullFever)
                return;
            ApplyNormalStats();
            isOnFullFever = false;
            if (feverFx != null)
            {
                feverFx.Stop();
            }
            OnFeverEnded?.Invoke();

        }

        public void OnFeverEffectExit()
        {

            if (!isOnFever)
                return;
            ApplyNormalStats();
            isOnFever = false;
            if (feverFx != null)
            {
                feverFx.Stop();
            }
            OnFeverEnded?.Invoke();
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

        public void OnStun()
        {
            if (isOnFever || isOnFullFever)
            {
                return;
            }
            isStuned = true;
            UpdateVisual();


        }

        public void OnAfterStun()
        {
            isStuned = false;
            UpdateVisual();
        }

        public void Active()
        {

        }



        #endregion
    }
}