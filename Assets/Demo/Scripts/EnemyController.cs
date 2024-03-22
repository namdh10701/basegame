using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] List<Transform> _posSpawnEnemy;
        [SerializeField] List<Enemy.Enemy> _prefabEnemies;

        List<Enemy.Enemy> _enemies = new List<Enemy.Enemy>();

        public void SpawnEnemy()
        {
            foreach (var item in _posSpawnEnemy)
            {
                var index = Random.Range(0, _prefabEnemies.Count);
                var enemy = Instantiate(_prefabEnemies[index], item);
                enemy.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                _enemies.Add(enemy);
            }
        }
    }
}
