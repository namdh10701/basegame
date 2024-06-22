using _Game.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Stash Item Data")]
public class StashItemData : ScriptableObject
{
    public List<GridItemData> gridItemDatas;
}


