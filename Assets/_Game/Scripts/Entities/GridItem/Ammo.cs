using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPG.Stats;
using _Base.Scripts.RPGCommon.Entities;
using _Base.Scripts.Shared;
using _Game.Features.Gameplay;
using _Game.Scripts.GD;
using _Game.Scripts.PathFinding;
using System;
using System.Collections.Generic;
using UnityEngine;
public enum AmmoType
{
    Standard, Bomb
}

namespace _Game.Scripts.Entities
{
    public class Ammo : Entity, IEffectTaker, IGridItem, IWorkLocation, INodeOccupier, IGDConfigStatsTarget
    {
        [SerializeField] private GridItemStateManager gridItemStateManager;
        [SerializeField] public GridItemView view;
        public string id;
        public GDConfig gdConfig;
        public StatsTemplate statsTemplate;
        public StatsTemplate StatsTemplate => statsTemplate;
        public CannonProjectile Projectile;

        public AmmoType AmmoType;

        [SerializeField] private GridItemDef def;

        [field: SerializeField]
        public List<Cell> OccupyCells { get; set; }
        public GridItemDef Def { get => def; }
        public Transform Behaviour { get => null; }
        public string GridId { get; set; }

        public List<Node> workingSlots = new List<Node>();
        public List<Node> WorkingSlots { get => workingSlots; set => workingSlots = value; }
        public List<Node> occupyingNodes = new List<Node>();
        public List<Node> OccupyingNodes { get => occupyingNodes; set => occupyingNodes = value; }
        public bool IsAbleToTakeHit { get => stats.HealthPoint.Value > stats.HealthPoint.MinValue; }
        public EffectHandler effectHandler;
        public EffectHandler EffectHandler => effectHandler;

        public override Stats Stats => stats;

        public Transform Transform => transform;

        public string Id { get => id; set => id = value; }
        public GDConfig GDConfig { get => gdConfig; }

        public Stat StatusResist => null;

        public GridItemStateManager GridItemStateManager => gridItemStateManager;

        public AmmoStats stats;




        public void SetId(string id)
        {
            this.id = id;
            Projectile.Id = id;
        }

        public void Initialize()
        {
            EffectHandler.EffectTaker = this;
            GDConfigStatsApplier GDConfigStatsApplier = GetComponent<GDConfigStatsApplier>();
            GDConfigStatsApplier.LoadStats(this);
            stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            gridItemStateManager.gridItem = this;
            view.Init(this);

        }

        private void HealthPoint_OnValueChanged(RangedStat stat)
        {
            if (stat.Value <= stat.MinValue)
            {
                gridItemStateManager.GridItemState = GridItemState.Broken;
            }
            else
            {
                gridItemStateManager.GridItemState = GridItemState.Active;
            }
        }

        public void OnBroken()
        {
            gridItemStateManager.GridItemState = GridItemState.Broken;
            GlobalEvent<Ammo, int>.Send("FixAmmo", this, CrewJobData.DefaultPiority[typeof(FixAmmoTask)]);
        }
        public void Active()
        {

        }

        public void OnFixed()
        {
            stats.HealthPoint.StatValue.BaseValue = stats.HealthPoint.MaxStatValue.Value / 100 * 30;
        }

        public override void ApplyStats() { }


    }
}