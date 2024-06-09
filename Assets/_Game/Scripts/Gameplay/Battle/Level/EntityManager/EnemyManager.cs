using _Game.Scripts.Entities;
using System.Collections;
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

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1.0f);
            foreach (var groupSpawnData in groupEnemySpawnData)
            {
                foreach (EnemySpawnData enemySpawnData in groupSpawnData.EnemySpawnDatas)
                {
                    if (enemySpawnData.EnemyId == 1)
                    {

                        enemySpawnTimer.RegisterEvent(new TimedEvent(enemySpawnData.Time, () => SpawnEnemy(enemySpawnData.EnemyId, CoordinateConverter.ToWorldPos(enemySpawnData.Position), enemySpawnData.TargetPosition)));
                    }
                    else
                    {

                        enemySpawnTimer.RegisterEvent(new TimedEvent(enemySpawnData.Time, () => SpawnEnemy(enemySpawnData.EnemyId, CoordinateConverter.ToWorldPos(enemySpawnData.Position), enemySpawnData.TargetPosition)));
                    }
                }
            }
            enemySpawnTimer.StartTimer();
        }


        public void SpawnEnemy(int id, Vector2 position, List<Vector2> targetPosition)
        {
            Enemy spawned = (Enemy)entityManager.SpawnEntity(enemyPrefabs[id - 1], position, Quaternion.identity, enemyRoot);
            if (spawned is RangedEnemy ranged)
            {
                List<Vector2> tp = new List<Vector2>();
                foreach (Vector2 v in targetPosition)
                {
                    tp.Add(CoordinateConverter.ToWorldPos(v));
                }
            }

            if (spawned is MeeleEnemy meele)
            {
                List<Vector2> tp = new List<Vector2>();
                foreach (Vector2 v in targetPosition)
                {
                    tp.Add(CoordinateConverter.ToWorldPos(v));
                }
            }
        }



    }
}
