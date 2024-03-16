using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsConfig", menuName = "WeaponsConfig/Data", order = 1)]
public class WeaponsConfig : ScriptableObject
{
    public List<WeaponData> WeaponsData;
}

[Serializable]
public class WeaponData
{
    public Color Color;
}