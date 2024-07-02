using _Base.Scripts.RPG.Stats;
using System.Collections.Generic;

namespace _Game.Scripts.InventorySystem
{
    public class Gear : IInventoryItem, IUpgradeable
    {
        private int id;
        private string name;
        private string description;
        private GearType gearType;
        private Rarity rarity;

        public List<Stat> stats;

        public int Id { get => id; }
        public string Name { get => name; }
        public string Description { get => description; }
        public GearType GearType { get => gearType; }
        public Rarity Rarity { get => rarity; set => rarity = value; }

        public Gear(GearData gearData)
        {
            id = gearData.Id;
        }
    }
}
