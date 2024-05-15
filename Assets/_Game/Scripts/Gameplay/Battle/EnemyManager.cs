
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay;
using Fusion;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class EnemyManager : MonoBehaviour
    {
        public List<GroupEnemySpawnData> groupEnemySpawnData;
        private List<GroupEnemySpawnData> spawned;

        [SerializeField] EntityManager entityManager;
        [SerializeField] Area spawnArea;
        public Enemy[] enemyPrefabs;
        [SerializeField] Timer enemySpawnTimer;
        [SerializeField] Transform enemyRoot;
        bool IsActive;

        private void Start()
        {
            foreach (var groupSpawnData in groupEnemySpawnData)
            {
                foreach (EnemySpawnData enemySpawnData in groupSpawnData.EnemySpawnDatas)
                {
                    enemySpawnTimer.RegisterEvent(new TimedEvent(enemySpawnData.Time, () => SpawnEnemy(enemySpawnData.EnemyId, enemySpawnData.Position)));
                }
            }
            enemySpawnTimer.StartTimer();
        }


        public void SpawnEnemy(int id, Vector2 position)
        {
            entityManager.SpawnEntity(enemyPrefabs[id - 1], position, Quaternion.identity, enemyRoot);
        }

    }
}
