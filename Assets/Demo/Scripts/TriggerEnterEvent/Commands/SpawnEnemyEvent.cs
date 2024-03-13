using UnityEngine;
public class SpawnEnemyEvent : MonoBehaviour, ITriggerEnterEvent
{
    [SerializeField] GameObject enemyPrefab;
    public void Execute()
    {
        Instantiate(enemyPrefab, Camera.main.transform.position, Quaternion.identity);
    }
}