using System.Collections.Generic;
using _Game.Features.Inventory;
using UnityEngine;
namespace _Base.Scripts.UI
{
    [System.Serializable]
    public class InventoryItemData
    {
        public int startX;
        public int startY;
        public string gridID;
        public Vector2 position;
        public string Id;
        public ItemType Type;
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<InventoryItemData> InventoryItemsOnGrid;
        public List<InventoryItemData> InventoryItemsOnStash;
    }
}
