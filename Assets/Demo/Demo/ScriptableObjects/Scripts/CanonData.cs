using UnityEngine;

namespace Demo.ScriptableObjects.Scripts
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Cannon Data")]
    public class CanonData : ScriptableObject
    {
        public int Id;
        public string GunName;
        public float Attack;
        public float AttackSpeed;
        public float Accuracy;
        public float CritChance;
        public float CritDamage;
    }
}
