using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuSetupConfig", menuName = "Scriptable Objects/Menu Setup Config", order = 1)]
public class MenuSetupConfig : ScriptableObject
{
    public List<ItemMenuData> itemGunDatas = new List<ItemMenuData>();
    public List<ItemMenuData> itemBulletDatas = new List<ItemMenuData>();
    public List<ItemMenuData> itemCharaterDatas = new List<ItemMenuData>();
    public List<ItemMenuData> itemSkinShipDatas = new List<ItemMenuData>();
}


[Serializable]
public class ItemMenuData
{
    public int id;
    public int numbCell;
    public Sprite sprite;
    public Vector2 sizeCollision;
    public ItemType itemType;
    public bool isSelected;
    public Color color = Color.white;

}

public enum ItemType
{
    Gun,
    Bullet,
    Sailor,
    SkinShip,
    None
}

public enum TabType
{
    Gun,
    Bullet,
    Sailor,
    SkinShip
}

[Serializable]
public class ItemMenuSkinShip
{
    public int id;
    public Sprite sprite;
    public Color color;
    public ItemType itemType;

}


