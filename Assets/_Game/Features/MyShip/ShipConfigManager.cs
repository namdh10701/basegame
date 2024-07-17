using System.Collections.Generic;
using _Game.Features.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Features.MyShip
{
    public class ItemPositionChangedEvent : UnityEvent<Dictionary<Vector2Int, InventoryItem>>
    {
    }

    public class ShipConfigManager : MonoBehaviour
    {
        public ItemPositionChangedEvent OnItemPositionChanged = new();

        public Dictionary<Vector2Int, InventoryItem> ItemPositions { get; set; } = new();

        public string InventoryItem;

    }
}
