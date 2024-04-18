
using UnityEngine;

namespace _Game.Scripts.Gameplay
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] EntityManager entityManager;
        [SerializeField] EnemySpawnArea spawnArea;
        [SerializeField] Enemy melleEnemy;
        [SerializeField] Enemy rangedEnemy;
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
