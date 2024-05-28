using System;
using System.Reflection;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Enemy_", menuName = "SO/Enemy", order = 1)] 
    [Serializable]
    public class EnemyConfig: GDConfig
    {
        public string ID;
        public string name;
        public float attack;
        public float attack_speed;
        public float hp;
        public float block_chance;

        public override string GetId() => ID;

        public override void ApplyGDConfig(object stats)
        {
            
        }
    }
}