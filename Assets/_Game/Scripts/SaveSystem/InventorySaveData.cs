using UnityEngine;
using _Game.Scripts.InventorySystem;
using System.Collections.Generic;

namespace _Game.Scripts.SaveLoad
{
    [System.Serializable]
    public class InventorySaveData
    {
        [SerializeReference] public List<IInventoryData> OwnedInventories = new List<IInventoryData>();
        public List<GearData> EquippingGears;
    }

    public interface IInventoryData
    {
        public int Id { get; set; }
        public InventoryType Type { get; }

    }

    [System.Serializable]
    public class GearData : IInventoryData, IUpgradeable
    {
        public int id;
        public InventoryType inventoryType;
        public GearType GearType;
        public Rarity rarity;

        public GearData(int id, GearType gearType, Rarity rarity)
        {
            this.id = id;
            this.inventoryType = InventoryType.Gear;
            this.GearType = gearType;
            this.rarity = rarity;
        }

        public int Id { get => id; set => id = value; }
        public InventoryType Type { get => inventoryType; }
        public Rarity Rarity { get => rarity; set => rarity = value; }
    }
}
