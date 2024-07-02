using System;
using System.Collections.Generic;
using _Game.Features.Inventory;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Inventory Items Received Define")]
    public class InventoryItemsReceivedDef : ScriptableObject
    {
        public InventoryItem InventoryItem;
        public List<InventoryItemsConfig> inventoryItemsReceived = new List<InventoryItemsConfig>();
    }


    [Serializable]
    public class InventoryItemsConfig
    {
        public ItemType Type;
        public List<InventoryItemInfo> inventoryItemsInfo = new List<InventoryItemInfo>();
    }

    [Serializable]
    public class InventoryItemInfo
    {
        public InventoryItemData inventoryItemData;
    }
}
