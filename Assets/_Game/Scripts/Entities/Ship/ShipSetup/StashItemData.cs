using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Stash Item Data")]
public class StashItemData : ScriptableObject
{
    public List<GridItemData> gridItemDatas;
}


