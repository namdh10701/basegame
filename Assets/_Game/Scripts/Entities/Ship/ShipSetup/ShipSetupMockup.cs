using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ship Setup Mockup")]
public class ShipSetupMockup : ScriptableObject
{
    public List<GridItemData> Datas = new List<GridItemData>();
}
