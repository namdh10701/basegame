using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections.Generic;
using _Base.Scripts.RPG.Stats;
using UnityEngine;
using _Game.Scripts.Gameplay.Ship;
using _Game.Scripts.Entities;

namespace _Game.Scripts.Battle
{
    public class EntityManager : MonoBehaviour
    {
        public List<Entity> aliveEntities = new List<Entity>();
        public Ship Ship;
        public Transform entityRoot;
        public Transform enemyRoot;
        public void SpawnShip(string id, Vector3 spawnPosition)
        {
            Ship ship = ResourceLoader.LoadShip(id);
            Ship = Instantiate(ship, entityRoot);
            Ship.transform.position = spawnPosition;
        }
        public void SpawnEnemy(string id, Vector3 position)
        {
            Enemy enemy = ResourceLoader.LoadEnemy(id);
            Enemy spawned = Instantiate(enemy, position, Quaternion.identity, enemyRoot);

            if (spawned.Stats is IAliveStats alive)
            {
                aliveEntities.Add(spawned);
                alive.HealthPoint.OnValueChanged += (newStat) => MaxHealthPoint_OnValueChanged(newStat, spawned);
            }
        }

        private void MaxHealthPoint_OnValueChanged(RangedStat newStat, Entity alive)
        {
            if (newStat.Value <= 0)
            {
                aliveEntities.Remove(alive);
                GlobalEvent<Entity>.Send("EntityDied", alive);
            }
        }

        public void CleanUp()
        {
            Destroy(Ship.gameObject);
            foreach (Entity e in aliveEntities)
            {
                if (e != null)
                    Destroy(e.gameObject);
            }
            aliveEntities.Clear();
        }
    }
}