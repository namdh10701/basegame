using UnityEngine;

namespace Demo.Scripts.TriggerEnterEvent.Commands
{
    public enum EnemyLayer
    {
        MoveAlongShip,
        Free
    }
    public class EnemySpawnData : MonoBehaviour
    {
        public int EnemyId;
        public EnemyLayer Layer;
    }
}