using System.Collections.Generic;
using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Behaviours.FindTarget;
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
using UnityEngine.UIElements;
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
        public FindTargetBehaviour FindTargetBehaviour;

        public Bullet usingBullet;
        public SpineAnimationCannonHandler Animation;
        protected override void Awake()
        {
            base.Awake();
        }
        public void DisableWhenActive()
        {
            Cell[,] shape = ConvertOccupyCellsToShape();
            NodeGraph nodeGraph = FindAnyObjectByType<NodeGraph>();
            List<Cell> cells = GridHelper.GetCellsAroundShape(OccupyCells[0].Grid.Cells, OccupyCells);

            foreach (Cell cell in cells)
            {
                foreach (Node node in nodeGraph.nodes)
                {
                    if (node.cell == cell)
                    {
                        disableWhenActive.Add(node);
                        node.State = WorkingSlotState.Disabled;
                    }
                }
            }

            List<Cell> bottomCells = new List<Cell>();
            for (int i = 0; i < shape.GetLength(1); i++)
            {
                bottomCells.Add(shape[0, i]);
            }

            List<Cell> canbeActive = GridHelper.GetCellsAroundShape(OccupyCells[0].Grid.Cells, bottomCells);
            foreach (Cell cell in canbeActive)
            {
                foreach (Node node in nodeGraph.nodes)
                {
                    if (node.cell == cell)
                    {
                        node.State = WorkingSlotState.Free;
                        disableWhenActive.Remove(node);
                    }
                }
            }
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
        public void OnClick()
        {
            GlobalEvent<Cannon, Bullet, int>.Send("Reload", this, usingBullet, int.MaxValue);
        }

        bool isOutOfAmmo;
        bool isBroken;
        void UpdateVisual()
        {
            if (isBroken)
            {
                Animation.PlayBroken();
            }
            if (isOutOfAmmo || isBroken)
            {
                FindTargetBehaviour.Disable();
                foreach (Node node in disableWhenActive)
                {
                    node.State = WorkingSlotState.Disabled;
                }
            }
            else
            {
                foreach (Node node in disableWhenActive)
                {
                    node.State = WorkingSlotState.Free;
                }
                FindTargetBehaviour.Enable();
            }


        }

        List<Node> disableWhenActive = new List<Node>();

        Cell[,] ConvertOccupyCellsToShape()
        {
            // Determine the bounds (dimensions) of the shape array
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            foreach (Cell cell in OccupyCells)
            {
                if (cell.X < minX)
                    minX = cell.X;
                if (cell.X > maxX)
                    maxX = cell.X;
                if (cell.Y < minY)
                    minY = cell.Y;
                if (cell.Y > maxY)
                    maxY = cell.Y;
            }
            int width = maxX - minX + 1;
            int height = maxY - minY + 1;
            Cell[,] shape = new Cell[height, width];
            foreach (Cell cell in OccupyCells)
            {
                int x = cell.X - minX;
                int y = cell.Y - minY;
                shape[y, x] = cell;
            }
            return shape;
        }

        public void OnOutOfAmmo()
        {
            isOutOfAmmo = true;
            GlobalEvent<Cannon, Bullet, int>.Send("Reload", this, usingBullet, 3);
            UpdateVisual();
        }

        public void OnReloaded()
        {
            isOutOfAmmo = false;
            UpdateVisual();
        }

        public void Deactivate()
        {
            isBroken = true;
            UpdateVisual();
        }

        public void OnFixed()
        {
            foreach (Cell cell in OccupyCells)
            {
                Debug.Log(cell.ToString() + " " + cell.stats.HealthPoint.IsFull);
                if (cell.stats.HealthPoint.Value == cell.stats.HealthPoint.MinValue)
                {
                    return;
                }
            }
            isBroken = false;
            UpdateVisual();
        }
    }
}