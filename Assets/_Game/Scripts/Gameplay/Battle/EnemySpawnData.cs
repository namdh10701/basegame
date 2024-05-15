using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GroupEnemySpawnData
{
    public List<EnemySpawnData> EnemySpawnDatas;
}

[Serializable]
public struct EnemySpawnData
{
    public int EnemyId;
    public int Time;
    public Vector2 Position;
}