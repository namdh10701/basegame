using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipConfig", menuName = "Scriptable Objects/Ship Config", order = 1)]
public class ShipConfig : ScriptableObject
{
    public Cell Cell;
    public List<Grid> Grids = new List<Grid>();
}


[Serializable]
public class Grid
{
    public Transform Transform;
    public int Rows;
    public int Cols;
}

