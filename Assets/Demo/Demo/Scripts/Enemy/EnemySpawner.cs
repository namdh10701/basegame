using System.Collections.Generic;
using _Base.Scripts.Utils;
using Demo.Scripts.TriggerEnterEvent.Commands;
using UnityEngine;

namespace Demo.Scripts.Enemy
{
    public class EnemySpawner : SingletonMonoBehaviour<EnemySpawner>
    {
        [SerializeField] Enemy[] enemyPrefabs;
        Dictionary<int, Enemy> enemiesDictionary = new Dictionary<int, Enemy>();
        [SerializeField] Transform moveAlongShipRoot;
        [SerializeField] Transform freeRoot;
        protected override void Awake()
        {
            base.Awake();
            AddEnemiesToDic();
        }

        void AddEnemiesToDic()
        {
            foreach (Enemy enemy in enemyPrefabs)
            {
                enemiesDictionary.Add(enemy.EnemyData.Id, enemy);
            }
        }

        public void SpawnEnemy(EnemySpawnData enemySpawnData)
        {
            Instantiate(enemiesDictionary[enemySpawnData.EnemyId], enemySpawnData.transform.position, Quaternion.identity, enemySpawnData.Layer == EnemyLayer.MoveAlongShip ? moveAlongShipRoot : freeRoot);
        }
    }
}
