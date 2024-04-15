using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipConfig", menuName = "Scriptable Objects/Ship Config", order = 1)]
public class ShipConfig : ScriptableObject
{

    public Cell cell;
    public List<GridData> grids = new List<GridData>();
}


[Serializable]
public class GridData
{
    public string id;
    public Transform transform;
    public int rows;
    public int cols;
}

