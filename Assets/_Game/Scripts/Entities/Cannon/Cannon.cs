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
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PathFinding;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Cannon : Entity, IFighter, IGridItem, IWorkLocation, INodeOccupier, IEffectTaker, IGDConfigStatsTarget
    {
        [Header("GD Config Stats Target")]
        public string id;
        public GDConfig gdConfig;
        public StatsTemplate statsTemplate;

        [Header("Cannon")]
        [field: SerializeField]
        private GridItemDef def;

        [SerializeField]
        private CannonStats _stats;

        [SerializeField]
        private CannonStatsTemplate _statsTemplate;

        public IFighterStats FighterStats
        {
            get => _stats;
            set => _stats = (CannonStats)value;
        }
        public CannonRenderer CannonRenderer;

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
        }

        #region Controller

        public CannonHUD HUD;
        bool isBroken;
        bool isOutOfAmmo;

        public void Initizalize()
        {
            GDConfigStatsApplier = GetComponent<GDConfigStatsApplier>();
            GDConfigStatsApplier.LoadStats(this);

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
            else if (stat.StatValue.Value == stat.MaxValue)
            {
                OnReloaded();
            }
        }

        private void HealthPoint_OnValueChanged(RangedStat stat)
        {
            if (stat.StatValue.Value == stat.MinValue)
            {
                OnBroken();
            }
        }

        public void OnOutOfAmmo()
        {
            if (usingBullet.AmmoType == AmmoType.Bomb && isOnFever)
            {
                OnFeverEffectExit();
            }

            isOutOfAmmo = true;
            GlobalEvent<Cannon, Ammo, int>.Send("Reload", this, usingBullet, 15);
            OutOfAmmo?.Invoke();
            UpdateVisual();
            FindTargetBehaviour.Disable();
        }
        public void OnBroken()
        {
            isBroken = true;
            GlobalEvent<IGridItem, int>.Send("Fix", this, 20);
            Broken?.Invoke();
            UpdateVisual();

            FindTargetBehaviour.Disable();
        }

        public void OnClick()
        {
            if (isOnFever)
                return;

            if (IsBroken)
            {
                GlobalEvent<IGridItem, int>.Send("Fix", this, int.MaxValue);
            }
            else
            {
                GlobalEvent<Cannon, Ammo, int>.Send("Reload", this, usingBullet, int.MaxValue);
            }
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
            if (!isOutOfAmmo && !isBroken)
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

            usingBullet = bullet;
            AmmoStats ammoStats = (AmmoStats)bullet.Stats;

            AttackTargetBehaviour.projectilePrefab = bullet.Projectile;
        }

        public bool IsOnFever => isOnFever;

        public ParticleSystem feverFx;
        public ParticleSystem feverEnterFx;
        bool isOnFever;
        public Action OnFeverStart;
        public Action OnFeverEnded;

        public void OnFeverEffectEnter()
        {
            isOnFever = true;
            feverEnterFx.Play();
            feverFx.Play();
            OnFeverStart?.Invoke();
            ApplyFeverStats();
            // code change stats go here 
            if (usingBullet.AmmoType == AmmoType.Bomb)
            {
                _stats.Ammo.StatValue.BaseValue = _stats.Ammo.MaxStatValue.BaseValue + 5;
            }
            Invoke("OnFeverEffectExit", 5);

        }
        public void OnFullFeverEffectEnter()
        {
            feverEnterFx.Play();
            feverFx.Play();
            isOnFever = true;
            ApplyFeverStats();
            if (usingBullet.AmmoType == AmmoType.Bomb)
            {
                _stats.Ammo.StatValue.BaseValue = _stats.Ammo.MaxStatValue.BaseValue + 10;
            }

            Invoke("OnFeverEffectExit", 10);
        }

        public void OnFeverEffectExit()
        {
            ApplyNormalStats();
            if (!isOnFever)
                return;

            isOnFever = false;
            feverFx.Stop();
            OnFeverEnded?.Invoke();
            // code update stats go here
        }

        void ApplyFeverStats()
        {
            if (GDConfigLoader.Instance.CannonFevers.TryGetValue(id, out CannonConfig value))
            {
                value.ApplyGDConfig(_stats);
            }
        }

        void ApplyNormalStats()
        {
            if (GDConfigLoader.Instance.Cannons.TryGetValue(id, out CannonConfig value))
            {
                value.ApplyGDConfig(_stats);
            }
        }

        #endregion
    }
}