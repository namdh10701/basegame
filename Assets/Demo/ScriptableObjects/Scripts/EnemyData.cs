
using UnityEngine;

namespace Demo.ScriptableObjects.Scripts
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public int Id;
        public float Attack;
        public float AttackSpeed;
    }
}
