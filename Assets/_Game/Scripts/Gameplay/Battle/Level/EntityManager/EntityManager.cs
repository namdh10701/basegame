using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Entities;
using _Game.Features.Gameplay;

namespace _Game.Scripts.Battle
{
    public class EntityManager : MonoBehaviour
    {
        public List<EnemyModel> aliveEnemies = new List<EnemyModel>();
        public Ship Ship;

        void OnEnemyDied(EnemyModel enemyModel)
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
            EnemyModel enemy = ResourceLoader.LoadEnemy(id);
            EnemyModel spawned = Instantiate(enemy, position, Quaternion.identity);
            aliveEnemies.Add(spawned);
        }

        public void OnEnter()
        {
            GlobalEvent<EnemyModel>.Register("EnemyDied", OnEnemyDied);
        }

        public void CleanUp()
        {
            GlobalEvent<EnemyModel>.Unregister("EnemyDied", OnEnemyDied);
            Destroy(Ship.gameObject);
            foreach (Entity e in aliveEnemies)
            {
                if (e != null)
                    Destroy(e.gameObject);
            }
        }
    }
}