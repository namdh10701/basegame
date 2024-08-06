using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Entities;
using _Game.Features.Gameplay;
using _Base.Scripts.RPGCommon.Entities;
using Fusion;
using System;

namespace _Game.Scripts.Battle
{
    public class EntityManager : MonoBehaviour
    {
        public List<EnemyStats> aliveEnemies = new List<EnemyStats>();
        public List<EnemyModel> enemyModels = new List<EnemyModel>();
        public List<SkullGang> skullGangs = new List<SkullGang>();
        public Ship Ship;
        public GiantOctopus octopusPrefab;
        public SkullGang skullGangPrefab;
        public Action AliveEnemiesChanged;
        public Transform enemyRoot;

        void OnEnemyDied(EnemyStats enemyModel)
        {
            if (aliveEnemies.Contains(enemyModel))
            {
                aliveEnemies.Remove(enemyModel);
                AliveEnemiesChanged?.Invoke();
            }

        }
        public void OnPlayAgain()
        {
            aliveEnemies.Clear();
            enemyRoot.gameObject.SetActive(false);
            enemyRoot = new GameObject().transform;
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
                GiantOctopus giantOctopus = Instantiate(octopusPrefab, position, Quaternion.identity, enemyRoot);
                aliveEnemies.Add(giantOctopus.Stats as EnemyStats);
            }
            else
            if (id == "0010")
            {
                SkullGang skullGang = Instantiate(skullGangPrefab, position, Quaternion.identity, enemyRoot);
                aliveEnemies.Add(skullGang.Stats as EnemyStats);
                skullGangs.Add(skullGang);
            }
            else
            {
                EnemyModel enemy = ResourceLoader.LoadEnemy(id);
                EnemyModel spawned = Instantiate(enemy, position, Quaternion.identity, enemyRoot);
                aliveEnemies.Add(spawned.Stats as EnemyStats);
                enemyModels.Add(spawned);
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

        public void Cleanup()
        {
            Debug.Log("CLEAN UP");
            foreach (EnemyModel enemyModel in enemyModels)
            {
                if (enemyModel != null)
                {
                    enemyModel.Disable();
                }
            }

            foreach (SkullGang skullGang in skullGangs)
            {
                if (skullGang != null)
                {
                    skullGang.Disable();
                }
            }

        }


    }
}