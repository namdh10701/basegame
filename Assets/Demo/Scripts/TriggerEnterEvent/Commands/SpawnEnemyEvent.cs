using Demo.Scripts.Enemy;
using UnityEngine;

namespace Demo.Scripts.TriggerEnterEvent.Commands
{
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
}