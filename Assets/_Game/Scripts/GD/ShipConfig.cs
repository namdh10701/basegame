using System;
using System.Reflection;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Ship_", menuName = "SO/Ship", order = 1)] 
    [Serializable]
    public class ShipConfig: GDConfig
    {
        public string ID;
        public string name;
        public float hp;
        public float block_chance;
        public float mana_regen_rate;

        public override string GetId() => ID;

        public override void ApplyGDConfig(object stats)
        {
            
        }
    }
}