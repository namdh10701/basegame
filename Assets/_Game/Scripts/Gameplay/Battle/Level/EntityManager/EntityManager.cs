using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Entities;
using _Game.Features.Gameplay;
using _Base.Scripts.RPGCommon.Entities;
using Fusion;

namespace _Game.Scripts.Battle
{
    public class EntityManager : MonoBehaviour
    {
        public List<EnemyStats> aliveEnemies = new List<EnemyStats>();
        public Ship Ship;
        public GiantOctopus octopusPrefab;
        void OnEnemyDied(EnemyStats enemyModel)
        {
            if (aliveEnemies.Contains(enemyModel))
            {
                aliveEnemies.Remove(enemyModel);
            }
        }

        public void SpawnShip(string id, Vector3 spawnPosition)
        {
            Ship ship = ResourceLoader.LoadShip(id);
            Ship = Instantiate(ship);
            Ship.transform.position = spawnPosition;
        }
        public void SpawnEnemy(string id, Vector3 position)
        {
            if (id == "9999")
            {
                GiantOctopus giantOctopus = Instantiate(octopusPrefab, position, Quaternion.identity, null);
                aliveEnemies.Add(giantOctopus.Stats as EnemyStats);
            }
            else
            {
                EnemyModel enemy = ResourceLoader.LoadEnemy(id);
                EnemyModel spawned = Instantiate(enemy, position, Quaternion.identity);
                aliveEnemies.Add(spawned.Stats as EnemyStats);
            }
        }

        private void Start()
        {
            GlobalEvent<EnemyStats>.Register("EnemyDied", OnEnemyDied);
        }

        private void OnDestroy()
        {
            GlobalEvent<EnemyStats>.Unregister("EnemyDied", OnEnemyDied);
        }
    }
}