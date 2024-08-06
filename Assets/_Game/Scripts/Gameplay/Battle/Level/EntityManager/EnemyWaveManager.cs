using _Game.Scripts.DB;
using System.Collections.Generic;
using System.Globalization;
using _Game.Scripts.GD.DataManager;
using UnityEngine;
using UnityEngine.UIElements;
using Map;
using GoogleMobileAds.Api;

namespace _Game.Scripts.Battle
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static string stageId = "0001";
        public static string floorId;
        public static NodeType nodeType;

        [SerializeField] List<LevelData> levelDatas;
        [SerializeField] EntityManager entityManager;

        [SerializeField] Area pufferFishSpawnArea;
        [SerializeField] Area electricEelSpawnArea;
        [SerializeField] Area squidSpawnArea;
        [SerializeField] Transform bossSpawnArea;
        [SerializeField] Area crabSpawnArea;
        [SerializeField] Vector3 jellyFishSpawnPoint;

        [SerializeField] Timer enemySpawnTimer;
        public bool IsRanking;
        public void Init()
        {
            LoadLevelEnemyData();
        }
        void LoadLevelEnemyData()
        {
            if (IsRanking)
            {
                //levelDatas = GameData.RankLevelWaveTable;
            }
            else
            {
                stageId = PlayerPrefs.GetString("currentStage");
                if (nodeType == NodeType.MinorEnemy)
                {
                    levelDatas = GameData.LevelWaveTable.GetLevelData(stageId, floorId);
                }
                else if (nodeType == NodeType.MiniBoss)
                {
                    levelDatas = GameData.LevelWaveTable.GetStageEliteData(stageId);
                }
            }
        }
        public void StartLevel()
        {
            enemySpawnTimer.Clear();
            if (levelDatas != null)
            {
                foreach (var spawnData in levelDatas)
                {
                    TimedEvent timedEvent = new TimedEvent(float.Parse(spawnData.TimeOffset, CultureInfo.InvariantCulture), () => SpawnEnemy(spawnData.EnemyId.ToArray(),
                        int.Parse(spawnData.TotalPower)));
                    enemySpawnTimer.RegisterEvent(timedEvent);
                }
                enemySpawnTimer.StartTimer();
            }
        }

        public bool IsLevelDone { get => enemySpawnTimer.timedEvents.Count == 0; }

        public void Spawn(string id)
        {
            entityManager.SpawnEnemy(id, crabSpawnArea.SamplePoint());
        }
        public void SpawnEnemy(string[] idPool, int total_power)
        {
            float currentPower = 0;
            while (currentPower < total_power)
            {
                string id = idPool[Random.Range(0, idPool.Length)];
                float enemyPower = Database.GetEnemyPower(id);
                currentPower += enemyPower;
                Vector3 position = Vector3.zero;
                switch (id)
                {
                    case "0001":
                    case "0006":
                    case "0007":
                    case "0008":
                    case "0009":
                        position = pufferFishSpawnArea.SamplePoint();
                        break;
                    case "0002":
                        position = electricEelSpawnArea.SamplePoint();
                        break;
                    case "0003":
                        position = squidSpawnArea.SamplePoint();
                        break;
                    case "0005":
                        position = jellyFishSpawnPoint;
                        break;
                    case "0010":
                    case "0011":
                        position = crabSpawnArea.SamplePoint();
                        Debug.LogError(position);
                        break;
                    case "9999":
                        position = bossSpawnArea.position;
                        break;
                }

                entityManager.SpawnEnemy(id, position);
            }
        }

        public void SpawnEnemy(string[] idPool, int total_power, Area spawnArea)
        {
            float currentPower = 0;

            while (currentPower < total_power)
            {
                string id = idPool[Random.Range(0, idPool.Length)];
                float enemyPower = Database.GetEnemyPower(id);
                currentPower += enemyPower;
                entityManager.SpawnEnemy(id, spawnArea.SamplePoint());
            }
        }

        public void StopSpawn()
        {
            enemySpawnTimer.Pause();

        }
    }
}
