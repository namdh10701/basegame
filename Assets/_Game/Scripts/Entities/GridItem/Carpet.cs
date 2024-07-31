using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using _Game.Scripts.PathFinding;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class Carpet : Entity, IEffectTaker, IGridItem, IWorkLocation, INodeOccupier, IGDConfigStatsTarget
    {
        public string id;
        public GDConfig gdConfig;
        public StatsTemplate statsTemplate;
        public StatsTemplate StatsTemplate => statsTemplate;
        public CannonProjectile Projectile;

        public AmmoType AmmoType;


        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public Transform Behaviour { get => null; }
        public string GridId { get; set; }

        public List<Node> workingSlots = new List<Node>();
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> occupyingNodes = new List<Node>();
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }
        public EffectHandler effectHandler;
        public EffectHandler EffectHandler => effectHandler;


        public Transform Transform => transform;

        public string Id { get => id; set => id = value; }
        public GDConfig GDConfig { get => gdConfig; }


        public Stat StatusResist => null;

        public bool IsBroken { get; set; }

        public override Scripts.Stats Stats => stats;



        [SerializeField] private GridItemStateManager gridItemStateManager;
        public GridItemStateManager GridItemStateManager => gridItemStateManager;

        public CarpetStats stats;

        public override void ApplyStats()
        {
            throw new System.NotImplementedException();
        }

        public void OnBroken()
        {

        }

        public void OnFixed()
        {

        }

        public void Active()
        {
        }
    }
}