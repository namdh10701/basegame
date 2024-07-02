using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts;
using UnityEngine;


[Serializable]
public class ShipData
{
    public string id { get; set; }
    public List<GridItem> ship_items { get; set; }
    public List<StashItem> backstash_items { get; set; }
}

[Serializable]
public class StashItem
{
    public int startX { get; set; }
    public int startY { get; set; }
    public GridItemDef gridItemDef;

    public StashItem(int startX, int startY, GridItemDef gridItemDef)
    {
        this.startX = startX;
        this.startY = startY;
        this.gridItemDef = gridItemDef;
    }
}

[Serializable]
public class GridItem
{
    public string id { get; set; }
    public string type { get; set; }
    public int startX { get; set; }
    public int startY { get; set; }
}
