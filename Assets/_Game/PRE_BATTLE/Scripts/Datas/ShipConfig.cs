using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShipConfig
{
    public TypeShip typeShip;
    public Cell cell;
    public List<GridData> grids;
    public List<WeaponItemData> weaponItemDatas = new List<WeaponItemData>();

}


[Serializable]
public class GridData
{
    public string id;
    public Transform transform;
    public int rows;
    public int cols;
}

[Serializable]
public class WeaponItemData
{
    public ItemMenuData itemMenuData;
    public string previousGridID;
    public Vector2 previousPosition;
}

public enum TypeShip
{
    None,
    Normal,
    Skin1,
    Skin2
}

