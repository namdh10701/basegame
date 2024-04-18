using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataShips", menuName = "Scriptable Objects/Data Ships", order = 1)]

public class DataShips : ScriptableObject
{
    public List<ShipConfig> ships;
}
