using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Behaviours.FindTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities.CannonComponent;
using _Game.Scripts.GD;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PathFinding;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class Cannon : Entity, IStatsBearer, IShooter, IGridItem, IWorkLocation, INodeOccupier, IEffectTaker, IGDConfigStatsTarget
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

        [SerializeField]
        private CannonReloader _cannonReloader;

        public CannonReloader Reloader => _cannonReloader;


        public IFighterStats FighterStats
        {
            get => _stats;
            set => _stats = (CannonStats)value;
        }
        public CannonRenderer CannonRenderer;

        [field: SerializeReference]
        public AttackStrategy AttackStrategy { get; set; }
        public Transform behaviour;
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; set => def = value; }
        public Transform Behaviour { get => behaviour; }
        public string GridId { get; set; }
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }
        public bool IsBroken { get => isBroken; set => isBroken = value; }
        public List<Effect> BulletEffects { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public EffectHandler effectHandler;
        public EffectHandler EffectHandler { get => effectHandler; }

        public Transform Transform => transform;

        public bool IsAbleToTakeHit { get => _stats.HealthPoint.Value > _stats.HealthPoint.MinValue; }
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

        protected void ApplyStats()
        {
            FindTargetCollider.SetRadius(_stats.AttackRange.Value);
            AttackTargetBehaviour.projectilePrefab = usingBullet.Projectile;
        }

        public void SetProjectile(CannonProjectile projectile)
        {
            AttackTargetBehaviour.projectilePrefab = projectile;
        }
        public void OnClick()
        {
            if (IsBroken)
            {
                Debug.Log("Click");
                GlobalEvent<IGridItem, int>.Send("Fix", this, int.MaxValue);
            }
            else
            {
                GlobalEvent<Cannon, Ammo, int>.Send("Reload", this, usingBullet, int.MaxValue);
            }
        }

        bool isOutOfAmmo;
        bool isBroken;
        void UpdateVisual()
        {
            if (isBroken)
            {
                Animation.PlayBroken();
            }
            else
            {
                Animation.PlayNormal();
            }
            if (isOutOfAmmo || isBroken)
            {
                FindTargetBehaviour.Disable();
            }
            else
            {
                FindTargetBehaviour.Enable();
            }
        }


        public void OnOutOfAmmo()
        {
            isOutOfAmmo = true;
            GlobalEvent<Cannon, Ammo, int>.Send("Reload", this, usingBullet, 15);

            UpdateVisual();
        }

        public void OnReloaded()
        {
            isOutOfAmmo = false;
            UpdateVisual();
        }

        public void Deactivate()
        {
            GlobalEvent<IGridItem, int>.Send("Fix", this, 20);
            isBroken = true;
            Debug.Log("Hereererr");
            UpdateVisual();
        }

        public void OnFixed()
        {
            _stats.HealthPoint.StatValue.BaseValue = _stats.HealthPoint.MaxStatValue.Value / 100 * 30;
            isBroken = false;
            UpdateVisual();
        }

        void IStatsBearer.ApplyStats()
        {
            CannonStats stst = Stats as CannonStats;
            FindTargetCollider.SetRadius(stst.AttackRange.BaseValue);
        }
    }
}