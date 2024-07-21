using UnityEngine;

namespace _Game.Features.Inventory
{
    public class ShipSetupUtils
    {
        public static ShipSetupItem GetShipSetupItemPrefab(InventoryItem item)
        {
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load<ShipSetupItem>(shapePath);

            if (!prefab)
            {
                Debug.Log($"Error loading ShipSetupItem: {shapePath}");
            }

            return prefab;
        }
    }
}