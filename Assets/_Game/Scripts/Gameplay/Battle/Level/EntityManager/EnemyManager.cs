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
        bool isStart;
        float delayTime = 5;
        float delayElapsedTime = 0;
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
            isStart = true;
            foreach (var spawnData in levelDesignConfigs)
            {
                TimedEvent timedEvent = new TimedEvent(spawnData.time_offset, () => SpawnEnemy(spawnData.enemy_id));
                enemySpawnTimer.RegisterEvent(timedEvent);
            }
            enemySpawnTimer.StartTimer();
        }

        public bool IsLevelDone { get => enemySpawnTimer.timedEvents.Count == 0; }

        public void CleanUp()
        {
            isStart = false;
            enemySpawnTimer.Clear();
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
