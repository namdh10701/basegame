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
}
