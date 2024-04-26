using _Game.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataShips", menuName = "Scriptable Objects/Data Ships", order = 1)]

public class DataShips : ScriptableObject
{
    public List<ShipConfig> ships;
}

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