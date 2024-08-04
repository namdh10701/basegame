using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellSplitItem : IGridItem
{
    //public List<ICellSplitItemComponent> Components { get; }
}

public interface ICellSplitItemComponent : IGridItem
{
    public int RelativeX { get; }
    public int RelativeY { get; }
}
