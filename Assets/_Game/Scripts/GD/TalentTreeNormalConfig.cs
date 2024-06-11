using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "TalentTreeNormal_", menuName = "SO/TalentTreeNormal", order = 1)]
    [Serializable]
    public class TalentTreeNormalConfig: GDConfig
    {
        public string id;
        public float gold_cost;
        public float stat_id;

        public override string GetId() => id;
        public override void ApplyGDConfig(object stats)
        {
           
        }
    }
}