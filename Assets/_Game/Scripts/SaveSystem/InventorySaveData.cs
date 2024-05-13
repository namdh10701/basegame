using UnityEngine;
using _Game.Scripts.InventorySystem;
using System.Collections.Generic;

namespace _Game.Scripts.SaveLoad
{
    [System.Serializable]
    public class InventorySaveData
    {
        [SerializeReference] public List<InventoryData> OwnedInventories;
        public List<GearData> EquippingGears;
    }

    [System.Serializable]
    public class InventoryData
    {
        public int Id;
        public InventoryType Type;

        public InventoryData(int id, InventoryType type)
        {
            Id = id;
            Type = type;
        }
    }

    [System.Serializable]
    public class GearData : InventoryData
    {
        public GearType GearType;

        public GearData(int id, GearType gearType) : base(id, InventoryType.Gear)
        {
            Id = id;
            GearType = gearType;
        }
    }
}
