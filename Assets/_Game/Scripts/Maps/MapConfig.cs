using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Map Config")]
public class MapConfig : ScriptableObject
{
    public Odds Odds;
    public FloorConfig[] AbsoluteFloor;
    public FloorConfig[] AppearAfter;
    public Vector2Int Grid;
    public Vector2 DimensionSize;
    public int StartNodeNumber;
}



[Serializable]
public struct FloorConfig
{
    public NodeType NodeType;
    public int FloorNumber;
}
[Serializable]
public struct Odds
{
    [Range(0, 1)] public float NormalEnemy;
    [Range(0, 1)] public float EliteEnemy;
    [Range(0, 1)] public float Relic;
    [Range(0, 1)] public float RestSite;
    [Range(0, 1)] public float Shop;
    [Range(0, 1)] public float Amory;
}