using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridItemType
{
    Cannon, Bullet, Crew, Relic
}
public class GridItemData
{
    public string GridId;
    public Vector3 position;
    public GridItemDef Def;
    public List<Cell> OccupyCells;
}
