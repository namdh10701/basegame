using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipConfig", menuName = "Scriptable Objects/Ship Config", order = 1)]
public class ShipConfig : ScriptableObject
{
    public Cell cell;
    public List<Grid> grids = new List<Grid>();
}


[Serializable]
public class Grid
{
    public Transform transform;
    public int rows;
    public int cols;
}

