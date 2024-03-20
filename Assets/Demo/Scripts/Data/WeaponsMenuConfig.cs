using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsMennuConfig", menuName = "WeaponsMennuConfig/Data", order = 1)]
public class WeaponsMennuConfig : ScriptableObject
{
    public List<WeaponData> WeaponsData;
}


[Serializable]
public class WeaponData
{
    public int Id;
    public Sprite Sprite;
    public Color Color;
}