using System;
using System.Collections.Generic;

[Serializable]
public struct GroupEnemySpawnData
{
    public List<EnemySpawnData> EnemySpawnDatas;
}

[Serializable]
public struct EnemySpawnData
{
    public string EnemyId;
    public float Time;
}