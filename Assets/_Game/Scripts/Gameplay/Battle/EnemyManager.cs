
using _Game.Scripts.Battle;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] EntityManager entityManager;
        [SerializeField] Area spawnArea;
        [SerializeField] Enemy melleEnemy;
        [SerializeField] Enemy rangedEnemy;
        bool IsActive;
        private void Start()
        {
            //InvokeRepeating("SpawnRanged", 3, 5);
        }
        public void SpawnMelle()
        {
            entityManager.SpawnEntity(melleEnemy, spawnArea.SamplePoint(), Quaternion.identity, null);
        }

        public void SpawnRanged()
        {
            entityManager.SpawnEntity(rangedEnemy, spawnArea.SamplePoint(), Quaternion.identity, null);
        }
    }
}
