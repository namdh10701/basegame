using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Inventory.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class InventoryItem
    {
        public List<Vector2Int> Shape { get; set; }
        public string Id { get; set; }
        public ItemType Type { get; set; }
    }
}