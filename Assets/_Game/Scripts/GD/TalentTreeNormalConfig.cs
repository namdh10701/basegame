using System;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "TalentTreeNormal_", menuName = "SO/TalentTreeNormal", order = 1)]
    [Serializable]
    public class TalentTreeNormalConfig: GDConfig
    {
        public string ID;
        public float gold_cost;
        public float atk;
        public float hp;
        public float max_mana;
        public float mana_regen;
        public float ship_slot;

        public override string GetId() => ID;
        public override void ApplyGDConfig(object stats)
        {
           
        }
    }
}