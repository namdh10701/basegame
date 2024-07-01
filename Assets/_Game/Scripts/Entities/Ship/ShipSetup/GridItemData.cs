using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridItemType
{
    Cannon, Bullet, Crew, Relic
}
[System.Serializable]
public struct GridItemData
{
    public string GridId;
    public Vector3 position;
    public int startX;
    public int startY;
    public GridItemDef Def;
    public List<Vector2Int> OccupyCells;
}
