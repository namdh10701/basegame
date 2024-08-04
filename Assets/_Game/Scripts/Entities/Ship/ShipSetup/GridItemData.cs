using _Game.Scripts;
using System.Collections.Generic;
using UnityEngine;

public enum GridItemType
{
    Cannon, Bullet, Crew, Relic, Carpet
}
[System.Serializable]
public struct GridItemData
{
    //New Required
    public string GridId;
    public string Id;
    public GridItemType GridItemType;
    public int startX;
    public int startY;

}
