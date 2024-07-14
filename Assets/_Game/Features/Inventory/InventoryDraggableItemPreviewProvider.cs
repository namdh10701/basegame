using _Game.Features.MyShip;
using UnityEngine;

namespace _Game.Features.Inventory
{
    public class InventoryDraggableItemPreviewProvider : DraggableItemPreviewProvider
    {
        public override Object GetPreviewItemPrefab()
        {
            var item = GetComponent<InventorySheetItem>().InventoryItem;
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load(shapePath);
            return prefab;
        }
        
        public DraggingItem GetDragItemPrefab(InventoryItem item)
        {
            var shapePath = $"SetupItems/SetupItem_{item.Type.ToString().ToLower()}_{item.OperationType}";
            var prefab = Resources.Load<DraggingItem>(shapePath);
            return prefab;
        }
    }
}