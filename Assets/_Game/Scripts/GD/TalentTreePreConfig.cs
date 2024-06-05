using System;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "TalentTreePre_", menuName = "SO/TalentTreePre", order = 1)]
    [Serializable]
    public class TalentTreePreConfig: GDConfig
    {
        public string ID;
        public string Premium;
        public float diamond_cost;
        public float atk;
        public float hp;
        public float max_mana;
        public float mana_regen;
        public float ship_slot;
        public float Crit_chance;
        public float Gold_earning;

        public override string GetId() => ID;
        public override void ApplyGDConfig(object stats)
        {
           
        }
    }
}