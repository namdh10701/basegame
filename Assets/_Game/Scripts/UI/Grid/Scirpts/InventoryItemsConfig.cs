using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Inventory Items Config")]
    public class InventoryItemsConfig : ScriptableObject
    {
        public InventoryItem InventoryItem;
        public List<InventoryItemInfo> inventoryItemsInfo = new List<InventoryItemInfo>();
    }

    [Serializable]
    public class InventoryItemInfo
    {
        public InventoryItemData inventoryItemData;
    }
}
