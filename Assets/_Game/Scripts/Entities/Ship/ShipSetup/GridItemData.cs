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
    public Vector3 position;
    public GridItemDef Def;
    //New Required
    public string GridId;
    public string Id;
    public GridItemType GridItemType;
    public List<Vector2Int> OccupyCells;
    public int startX;
    public int startY;

}
