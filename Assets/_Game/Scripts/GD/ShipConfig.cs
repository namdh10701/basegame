using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Ship_", menuName = "SO/Ship", order = 1)] 
    [Serializable]
    public class ShipConfig: GDConfig
    {
        public string id;
        public string name;
        public float hp;
        public float block_chance;
        public float mana_regen_rate;

        public override string GetId() => id;

        public override void ApplyGDConfig(object stats)
        {
            
        }
    }
}