using System;
using UnityEngine;

namespace _Game.Scripts.GD
{
    [CreateAssetMenu(fileName = "Crew_", menuName = "SO/Crew", order = 1)]
    [Serializable]
    public class CrewConfig : GDConfig
    {
        public string id;
        public string name;
        public string rarity;
        public float move_speed;
        public float repair_speed;

        public override string GetId() => id;

        public override void ApplyGDConfig(object stats)
        {
            CrewStats crewStats = (CrewStats)stats;
            crewStats.MoveSpeed.BaseValue = move_speed;
            crewStats.RepairSpeed.BaseValue = repair_speed;
        }
    }
}