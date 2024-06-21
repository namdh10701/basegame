using System.Collections.Generic;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts.Entities.CannonComponent;
using _Game.Scripts.GD;
using _Game.Scripts.InventorySystem;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Game.Scripts.Entities
{
    public class Cannon : Entity, IGridItem, IShooter, IUpgradeable, IWorkLocation
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
        public List<WorkingSlot> WorkingSlots { get => workingSlots; set => workingSlots = value; }

        public List<WorkingSlot> workingSlots = new List<WorkingSlot>();

        public ObjectCollisionDetector FindTargetCollider;
        public AttackTargetBehaviour AttackTargetBehaviour;
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
        }

        public void SetProjectile(CannonProjectile projectile)
        {
            AttackTargetBehaviour.projectilePrefab = projectile;
        }



    }
}