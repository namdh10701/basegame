
using _Game.Scripts.Battle;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] EntityManager entityManager;
        [SerializeField] Area spawnArea;
        [SerializeField] Demo.Scripts.Enemy.Enemy melleEnemy;
        [SerializeField] Demo.Scripts.Enemy.Enemy rangedEnemy;
        bool IsActive;

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
