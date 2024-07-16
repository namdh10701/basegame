using _Game.Scripts.DB;
using System.Collections.Generic;
using System.Globalization;
using _Game.Scripts.GD.DataManager;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Scripts.Battle
{
    public class EnemyManager : MonoBehaviour
    {
        public static string stageId = "0001";
        public static string floorId;


        [SerializeField] List<LevelData> levelDatas;
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
            Debug.Log("AWAKE");
            stageId = PlayerPrefs.GetString("currentStage");

            Debug.Log(stageId);
            levelDatas = GameData.LevelWaveTable.GetLevelData(stageId, floorId);
        }
        public void StartLevel()
        {
            foreach (var spawnData in levelDatas)
            {
                TimedEvent timedEvent = new TimedEvent(float.Parse(spawnData.TimeOffset, CultureInfo.InvariantCulture), () => SpawnEnemy(spawnData.EnemyId.ToArray(),
                    int.Parse(spawnData.TotalPower)));
                enemySpawnTimer.RegisterEvent(timedEvent);
            }
            enemySpawnTimer.StartTimer();
        }

        public bool IsLevelDone { get => enemySpawnTimer.timedEvents.Count == 0; }

        public void CleanUp()
        {
            enemySpawnTimer.Clear();
        }
        public void Spawn(string id)
        {
            entityManager.SpawnEnemy(id, Vector3.zero);
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

                }

                entityManager.SpawnEnemy(id, position);
            }
        }




    }
}
