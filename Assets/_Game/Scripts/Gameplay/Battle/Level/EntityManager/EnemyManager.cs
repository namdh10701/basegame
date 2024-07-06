using _Base.Scripts.Utils.Extensions;
using _Game.Scripts.DB;
using _Game.Scripts.GD;
using _Game.Scripts.GD.Parser;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

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
        bool isStart;
        float delayTime = 5;
        float delayElapsedTime = 0;
        private void Awake()
        {
            LoadLevelEnemyData();
        }

        void LoadLevelEnemyData()
        {
            levelDatas = GameLevelManager.GetLevelData(stageId, floorId);
            //Debug.Log(selectLevelData.);
        }
        public void StartLevel()
        {
            isStart = true;
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
            isStart = false;
            enemySpawnTimer.Clear();
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
}
