using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Base.Scripts.Shared;
using _Game.Scripts.Entities.CannonComponent;
using _Game.Scripts.GD;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PathFinding;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Assertions;
using YamlDotNet.Core.Tokens;

namespace _Game.Scripts.Entities
{
    public class Cannon : Entity, IGridItem, IShooter, IUpgradeable, IWorkLocation, INodeOccupier
    {
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

        [field: SerializeReference]
        public AttackStrategy AttackStrategy { get; set; }

        [field: SerializeField]
        public List<Effect> BulletEffects { get; set; } = new();

        public Transform behaviour;
        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; set => def = value; }
        public Transform Behaviour { get => behaviour; }
        public string GridId { get; set; }

        public Rarity rarity;
        public Rarity Rarity { get => rarity; set => rarity = value; }
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }

        public List<Node> workingSlots = new List<Node>();
        public List<Node> occupyingNodes = new List<Node>();

        public ObjectCollisionDetector FindTargetCollider;
        public AttackTargetBehaviour AttackTargetBehaviour;

        public Bullet usingBullet;
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void LoadStats()
        {
            if (GDConfigLoader.Instance != null)
            {
                if (GDConfigLoader.Instance.Cannons.TryGetValue(def.Id, out CannonConfig cannonConfig))
                {
                    cannonConfig.ApplyGDConfig(_stats);
                }
            }
            else
            {
                _statsTemplate.ApplyConfig(_stats);
            }
        }

        protected override void LoadModifiers()
        {

        }
        protected override void ApplyStats()
        {
            FindTargetCollider.SetRadius(_stats.AttackRange.Value);
            AttackTargetBehaviour.projectilePrefab = usingBullet.Projectile;
        }

        public void SetProjectile(CannonProjectile projectile)
        {
            AttackTargetBehaviour.projectilePrefab = projectile;
        }

        public void OnOutOfAmmo()
        {
            GlobalEvent<Cannon, Bullet, int>.Send("Reload", this, usingBullet, 3);
        }

        public void OnClick()
        {
            GlobalEvent<Cannon, Bullet, int>.Send("Reload", this, usingBullet, int.MaxValue);
        }

        public void OnBroken()
        {
            FindTargetCollider.enabled = false;
        }

        public void OnFixed()
        {
            FindTargetCollider.enabled = true;
        }
    }
}