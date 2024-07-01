using _Game.Scripts.Entities;
using _Game.Scripts.GD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Scripts.Battle
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] List<LevelDesignConfig> levelDesignConfigs;
        [SerializeField] EntityManager entityManager;

        [SerializeField] Area pufferFishSpawnArea;
        [SerializeField] Area electricEelSpawnArea;
        [SerializeField] Area squidSpawnArea;
        [SerializeField] Vector3 jellyFishSpawnPoint;

        [SerializeField] Timer enemySpawnTimer;


        private void Awake()
        {
            LoadLevelEnemyData();
        }

        void LoadLevelEnemyData()
        {
            if (LevelDesignConfigLoader.Instance != null)
            {
                levelDesignConfigs = LevelDesignConfigLoader.Instance.LevelDesignConfigs;
            }
        }
        public void StartLevel()
        {
            foreach (var spawnData in levelDesignConfigs)
            {
                TimedEvent timedEvent = new TimedEvent(spawnData.time_offset, () => SpawnEnemy(spawnData.enemy_id));
                enemySpawnTimer.RegisterEvent(timedEvent);
            }
            enemySpawnTimer.StartTimer();
        }

        public void SpawnEnemy(string id)
        {
            Vector3 position = Vector3.zero;
            switch (id)
            {
                case "0001":
                    position = pufferFishSpawnArea.SamplePoint();
                    break;
                case "0002":
                    position = electricEelSpawnArea.SamplePoint();
                    break;
                case "0003":
                    position = squidSpawnArea.SamplePoint();
                    break;
                case "0004":
                    position = jellyFishSpawnPoint;
                    break;
            }

            entityManager.SpawnEnemy(id, position);

        }




    }
}
