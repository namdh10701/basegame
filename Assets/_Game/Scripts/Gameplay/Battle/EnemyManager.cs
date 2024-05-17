
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay;
using Fusion;
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
                    Debug.Log(CoordinateConverter.ToWorldPos(enemySpawnData.Position));
                    enemySpawnTimer.RegisterEvent(new TimedEvent(enemySpawnData.Time, () => SpawnEnemy(enemySpawnData.EnemyId, CoordinateConverter.ToWorldPos(enemySpawnData.Position), enemySpawnData.TargetPosition)));
                }
            }
            enemySpawnTimer.StartTimer();
        }


        public void SpawnEnemy(int id, Vector2 position, Vector2 targetPosition)
        {
            entityManager.SpawnEntity(enemyPrefabs[id - 1], position, Quaternion.identity, enemyRoot);
        }



    }
}
