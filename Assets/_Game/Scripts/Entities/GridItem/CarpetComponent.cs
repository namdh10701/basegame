using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetComponent : MonoBehaviour, IStatsBearer, ICellSplitItemComponent, IWorkLocation, IEffectTaker
{
    [SerializeField] private GridItemStateManager gridItemStateManager;
    [SerializeField] int relativeX;
    [SerializeField] int relativeY;

    public Carpet carpet;
    [SerializeField] CarpetComponentStats stats;
    [SerializeField] string gridId;
    public int RelativeX => relativeX;
    public int RelativeY => relativeY;
    public Stats Stats => stats;
    public string GridId { get => gridId; set => gridId = value; }
    public List<Node> workingSlots = new List<Node>();
    public List<Node> WorkingSlots { get => OccupyCells[0].WorkingSlots; set => workingSlots = value; }
    public bool IsAbleToTakeHit { get => false; }
    public EffectHandler effectHandler;
    public EffectHandler EffectHandler => effectHandler;
    public Transform Transform => transform;
    public GridItemStateManager GridItemStateManager => gridItemStateManager;

    public List<Cell> OccupyCells { get; set; }

    public bool IsWalkAble => true;

    public Stat StatusResist => null;

    public List<IGridItem> adjItems = new List<IGridItem>();
    public List<IGridItem> AdjItems { get => adjItems; set => adjItems = value; }

    private void Awake()
    {
        effectHandler.EffectTaker = this;
        gridItemStateManager.gridItem = this;
        stats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
    }

    private void HealthPoint_OnValueChanged(RangedStat obj)
    {
        if (obj.Value <= obj.MinValue)
        {
            gridItemStateManager.GridItemState = _Game.Scripts.Entities.GridItemState.Broken;
            OccupyCells[0].CellRenderer.OnBroken();
        }
        else if (obj.Value == obj.MaxValue)
        {
            gridItemStateManager.GridItemState = _Game.Scripts.Entities.GridItemState.Active;
            OccupyCells[0].CellRenderer.OnFixed();
        }
    }

    public void Active()
    {

    }

    public void OnBroken()
    {

    }

    public void ApplyStats()
    {

    }
}
