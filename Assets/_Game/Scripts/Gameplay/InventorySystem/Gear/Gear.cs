using _Base.Scripts.RPG.Stats;
using _Game.Scripts.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class Gear : InventoryItem, IUpgradeable
    {
        public GearType GearType;
        public Rarity Rarity { get; set; }
        public List<Stat> stats;
        public Gear(int id, string name, GearType gearType, Rarity rarity, List<Stat> stats) : base(id, name)
        {
            Type = InventoryType.Gear;
            GearType = gearType;
            Rarity = rarity;
            this.stats = stats;
        }

        public Gear(GearData gearData)
        {
            Id = gearData.Id;
            Name = "";
            Type = InventoryType.Gear;
        }
    }
}
