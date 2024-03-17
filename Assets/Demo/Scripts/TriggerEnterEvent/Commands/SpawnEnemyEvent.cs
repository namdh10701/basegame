using UnityEngine;
public class SpawnEnemyEvent : MonoBehaviour, ITriggerEnterEvent
{
    [SerializeField] EnemySpawnData[] enemySpawnDatas;
    public void Execute()
    {
        foreach(EnemySpawnData enemySpawnData in enemySpawnDatas)
        {
            EnemySpawner.Instance.SpawnEnemy(enemySpawnData);
        }
    }
}