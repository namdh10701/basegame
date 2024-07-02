using System;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "TalentTreePre_", menuName = "SO/TalentTreePre", order = 1)]
    [Serializable]
    public class TalentTreePreConfig: GDConfig
    {
        public string id;
        public string premium;
        public float diamond_cost;
        public string stat_id;

        public override string GetId() => id;
        public override void ApplyGDConfig(object stats)
        {
           
        }
    }
}